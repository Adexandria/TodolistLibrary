using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;
using NHibernate.MiniProfiler;
using NHibernate.Tool.hbm2ddl;
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
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(UserMap).Assembly))
                .ExposeConfiguration(cfg =>
                {
                    //new SchemaUpdate(cfg).Execute(true, true);
                   cfg.SetProperty("generate_statistics", "true");
                });
            return configuration.BuildSessionFactory();
        }

    }
}
