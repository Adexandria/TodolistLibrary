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

        public SessionFactory(string _connectionString, Assembly assembly)
        {
            mappingAssembly = assembly ?? typeof(UserMap).Assembly;
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
                   /* c.UseQueryCache().UseSecondLevelCache().ProviderClass("NHibernate.Cache.HashtableCacheProvider, NHibernate");
                    c.UseMinimalPuts();*/
                }
                )
                .ExposeConfiguration(cfg =>
                {
                    new SchemaExport(cfg).Create(true, true);
                   //cfg.SetProperty("generate_statistics", "true");
                });
              AddMappings(configuration);
            return configuration.BuildSessionFactory();
        }

        private void AddMappings(FluentConfiguration configuration)
        {
            if (mappingAssembly == typeof(UserMap).Assembly)
            {
                configuration.Mappings(x => x.FluentMappings.AddFromAssembly(mappingAssembly));
                return;
            }
           
            var currentTypes = typeof(UserMap).Assembly.GetTypes().Where(s=>s.BaseType?.Name == typeof(ClassMapping<>).Name);

            var types = mappingAssembly.GetTypes().Where(s=>s.BaseType.Name == typeof(ClassMap<>).Name);

            foreach(var currentType in currentTypes)
            {
                var type = types.FirstOrDefault(t => t.BaseType?.GetGenericArguments()[0].GetInterfaces()[0] == currentType.BaseType?.GetGenericArguments()[0]);
                if (type != null)
                    configuration.Mappings(x=> x.FluentMappings.Add(type));
                else
                {
                    configuration.Mappings(x=>x.FluentMappings.Add(currentType));
                }
            }
        }


        private readonly Assembly mappingAssembly;
    }
}
