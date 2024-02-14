using Autofac;
using NHibernate;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Architecture.TaskModule;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.DB;
using System.Reflection;
using TasksLibrary.Architecture.Extensions;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Application
{
    public static class TaskContainerBuilder
    {
        public static ContainerBuilder RegisterDependencies(string connectionString, params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            try
            {
               builder.Register((o) => new SessionFactory(connectionString, assemblies))
                        .PropertiesAutowired()
                        .SingleInstance();

                builder.RegisterType<AccessManagement>()
               .PropertiesAutowired()
              .InstancePerLifetimeScope();

                builder.Register((_) => new AppAssembly(assemblies))
                    .SingleInstance();

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

                builder.RegisterType<MapCommand>()
                    .PropertiesAutowired()
                    .SingleInstance();

                builder.Register((o) =>
                {
                    var factoryInstance = o.Resolve<SessionFactory>();
                    return factoryInstance.Session;
                }).As<ISession>().InstancePerLifetimeScope();

                builder.RegisterHandler(typeof(QueryHandler<,,>), 
                    typeof(IQueryHandler<,>),
                    new Assembly[] { typeof(QueryHandler<>).Assembly});

                builder.RegisterHandler(typeof(IQueryHandler<,>), 
                    typeof(IQueryHandler<,>),
                    new Assembly[] { typeof(QueryHandler<>).Assembly });

                builder.RegisterHandler(typeof(CommandHandler<,,>), 
                    typeof(ICommandHandler<,>), 
                    new Assembly[] { typeof(QueryHandler<>).Assembly });

                builder.RegisterHandler(typeof(CommandHandler<,>), 
                    typeof(ICommandHandler<>),
                    new Assembly[] { typeof(QueryHandler<>).Assembly});

                if (assemblies.Length > 0)
                {
                    builder.UseCustomDependencies(assemblies);
                }
                else
                {
                    builder.UseDefaultDependencies();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message,ex);
            }
           
            return builder;
        }
    }
}
