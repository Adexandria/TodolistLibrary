using Autofac;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Architecture.TaskModule;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.DB;
using TasksLibrary.Services;
using System.Reflection;
using TasksLibrary.Architecture.Extensions;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Application
{
    public class TaskContainerBuilder
    {
        public TaskContainerBuilder(string _connectionString)
        {
            ConnectionString = _connectionString;         
        }

        public ContainerBuilder RegisterDependencies(bool useDefaultConfiguration = true, params string[] assemblies)
        {
            var builder = new ContainerBuilder();

            builder.Register((o) => new SessionFactory(ConnectionString,assemblies))
                .PropertiesAutowired()
                .SingleInstance();

            builder.RegisterType<QueryContext<Note>>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccessManagement>()
                .PropertiesAutowired()
               .InstancePerLifetimeScope();

            builder.RegisterType<TaskManagement>()
                .PropertiesAutowired()
               .InstancePerLifetimeScope();

            builder.RegisterType<AccountManagementContext>()
                .As<DbContext<AccessManagement>>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<TaskManagementContext>()
              .As<DbContext<TaskManagement>>()
              .PropertiesAutowired()
              .InstancePerLifetimeScope();

            builder.Register((o) =>
            {
                var factoryInstance = o.Resolve<SessionFactory>();
                return factoryInstance.Session;
            }).As<ISession>().InstancePerLifetimeScope();


            if (!useDefaultConfiguration && assemblies.Length > 0)
            {
                builder.UseCustomDependencies(assemblies); 
            }
            else
            {
                builder.UseDefaultDependencies();
            }

            return builder;
        }



       /* private ServiceProvider BuildMigrationRunner()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(s=>s
                .AddSqlServer2012()
                .WithGlobalConnectionString(ConnectionString)
                .ScanIn(typeof(User).Assembly))
                .BuildServiceProvider(false);
        }*/
        public string ConnectionString { get; set; }
    }
}
