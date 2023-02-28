using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksLibrary.NHibernate.Mappings;

namespace TasksLibrary.NHibernate
{
    public class SessionFactory
    {
        public SessionFactory()
        {
            if (_sessionFactory is null)
            {
                string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Database = Tasks;Integrated Security=True;Connect Timeout=30";
                _sessionFactory = BuildSessionFactory(_connectionString);
            }
        }

        public ISession GetSession() => _sessionFactory.OpenSession();

        private readonly ISessionFactory _sessionFactory;

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
