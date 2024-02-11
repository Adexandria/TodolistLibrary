using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Driver;
using NHibernate.MiniProfiler;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
using System.Runtime.CompilerServices;
using TasksLibrary.DB.Mappings;

namespace TasksLibrary.DB
{
    public class SessionFactory : IFactoryBuilder
    {
        public SessionFactory(string _connectionString)
        {
            if (_sessionFactory is null || !string.IsNullOrEmpty(_connectionString))
                _sessionFactory = BuildSessionFactory(_connectionString);
        }

        public SessionFactory(string _connectionString, string[] assemblies)
        {
            _assemblies = assemblies;
            if (_sessionFactory is null || !string.IsNullOrEmpty(_connectionString))
                _sessionFactory = BuildSessionFactory(_connectionString);  
        }

        private ISessionFactory _sessionFactory;
        public ISession Session => _sessionFactory.OpenSession();
        public long GetStatistics()
        {
            return _sessionFactory.Statistics.SecondLevelCacheHitCount;
        }
        private ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString)
                .ShowSql()
                .FormatSql()
                .Driver<ProfiledDriver<SqlClientDriver>>())
                .Cache(c=> 
                {
                    c.UseQueryCache().UseSecondLevelCache().ProviderClass("NHibernate.Cache.HashtableCacheProvider, NHibernate");
                    c.UseMinimalPuts();
                }
                )
                .ExposeConfiguration(cfg =>
                {
                    new SchemaExport(cfg).Create(true, true);
                   cfg.SetProperty("generate_statistics", "true");
                });
              AddMappings(configuration);
            return configuration.BuildSessionFactory();
        }

        private void AddMappings(FluentConfiguration configuration)
        {
            if (_assemblies.Length == 0)
            {
                configuration.Mappings(x => x.FluentMappings.AddFromAssembly(typeof(UserMap).Assembly));
                return;
            }
            //specify the base type to be of ientity and throw an exception
            var currentTypes = typeof(UserMap).Assembly.GetTypes().Where(s=>s.BaseType?.Name == typeof(ClassMapping<>).Name);

            var executingTypes = GetExecutingTypes();

            foreach(var currentType in currentTypes)
            {
                var executingType = executingTypes.FirstOrDefault(t => t.BaseType?.GetGenericArguments()[0]?.GetInterfaces()[0] == currentType.BaseType?.GetGenericArguments()[0]);
                if (executingType != null)
                    configuration.Mappings(x=> x.FluentMappings.Add(executingType));
                else
                {
                    configuration.Mappings(x=>x.FluentMappings.Add(currentType));
                }
            }
        }

        private IEnumerable<Type> GetExecutingTypes()
        {
            IEnumerable<Type> executingTypes = null;
            foreach (var assemby in _assemblies)
            {
                var currentAssembly = Assembly.Load(_assemblies[0]);
                executingTypes = currentAssembly.GetTypes().Where(s => s.BaseType?.Name == typeof(ClassMap<>).Name);
                if (executingTypes != null)
                     break;
            }
            return executingTypes;
        }

        private readonly string[] _assemblies;
    }
}
