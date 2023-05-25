using Autofac;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Extensions;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.NHibernate;
using TasksLibrary.Services.Architecture.Database;
using TasksLibrary.Services.Architecture.TaskModule;

namespace TasksLibrary.Services.Architecture.Application
{
    public class TaskContainerBuilder
    {
        public TaskContainerBuilder(string _connectionString)
        {
            ConnectionString = _connectionString;         
        }

        public IContainer Build()
        {
            using (var service = BuildMigrationRunner())
            {
                using (var scope = service.CreateScope())
                {
                    var runner = service.GetRequiredService<IMigrationRunner>();
                    runner.MigrateUp();
                }
            }
           return SetUpDepedencies().Build();
        }
        private ContainerBuilder SetUpDepedencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccessTokenRepository>()
                .As<IAccessTokenRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<RefreshTokenRepository>()
                .As<IRefreshTokenRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthTokenRepository>()
                .As<IAuthToken>()
                .InstancePerLifetimeScope();

            builder.Register((o) => new SessionFactory(ConnectionString))
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


            builder.RegisterAssemblyTypes(typeof(CreateUserCommandHandler).Assembly)
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            return builder;
        }


        private ServiceProvider BuildMigrationRunner()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(s=>s
                .AddSqlServer2012()
                .WithGlobalConnectionString(ConnectionString)
                .ScanIn(typeof(User).Assembly))
                .BuildServiceProvider(false);
        }
        public string ConnectionString { get; set; }
    }
}
