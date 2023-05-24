using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using TasksLibrary.NHibernate.Mappings;

namespace TasksLibrary.NHibernate
{
    public class SessionFactory
    {
        public SessionFactory(string _connectionString)
        {
            if (_sessionFactory is null);
                _sessionFactory = BuildSessionFactory(_connectionString);
        }

        public ISessionFactory _sessionFactory;
        public ISession Session => _sessionFactory.OpenSession();

        private ISessionFactory BuildSessionFactory(string connectionString)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(typeof(UserMap).Assembly))
                .ExposeConfiguration(cfg =>
                {
                    new SchemaUpdate(cfg).Execute(true, true);
                });
            return configuration.BuildSessionFactory();
        }

    }
}
