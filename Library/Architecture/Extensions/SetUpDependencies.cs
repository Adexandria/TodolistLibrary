using Autofac;
using System.Reflection;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Extensions
{
    public static class SetUpDependencies
    {
        public static void UseDefaultDependencies(this ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>()
                .As<IUserRepository>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<AccessTokenRepository>()
                .As<IAccessTokenRepository>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<RefreshTokenRepository>()
                .As<IRefreshTokenRepository>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<AuthTokenRepository>()
                .As<IAuthToken>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<PasswordManager>()
                .As<IPasswordManager>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryContext<Note>>()
                .PropertiesAutowired()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }

        public static void UseCustomDependencies(this ContainerBuilder builder, Assembly[] assemblies)
        {
                builder.RegisterRepositories(typeof(IRepository<>), assemblies);

                builder.RegisterAuthentication(typeof(IAuthToken), assemblies);

                builder.RegisterAuthentication(typeof(IPasswordManager), assemblies);

                builder.RegisterHandler(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>), assemblies);

                builder.RegisterHandler(typeof(QueryHandler<,,>), typeof(IQueryHandler<,>), assemblies);

                builder.RegisterHandler(typeof(CommandHandler<,,>), typeof(ICommandHandler<,>), assemblies);

                builder.RegisterHandler(typeof(CommandHandler<,>), typeof(ICommandHandler<>), assemblies);
        }
       public static void RegisterHandler(this ContainerBuilder builder, Type dependencyType, Type interfaceType, Assembly[] assemblies)
        {
            Type executingType = null;
            foreach (var assembly in assemblies)
            {
                if(!dependencyType.IsInterface) 
                {
                    builder
                   .RegisterAssemblyTypes(assembly)
                   .Where(t => t.IsClosedTypeOf(dependencyType))
                   .As(type => FilterHandlers(type, interfaceType))
                   .AsClosedTypesOf(dependencyType)
                   .PropertiesAutowired()
                   .InstancePerLifetimeScope();
                }
                else
                {
                    builder
                  .RegisterAssemblyTypes(assembly)
                  .Where(t => t.IsClosedTypeOf(dependencyType))
                  .As(type => FilterHandlers(type, interfaceType))
                  .PropertiesAutowired()
                  .InstancePerLifetimeScope();
                }
            }
        }


        private static void RegisterAuthentication(this ContainerBuilder builder, Type dependencyType, Assembly[] assemblies)
        {
            var executingType = GetType(dependencyType, assemblies);
            if (executingType != null)
            {
                builder.RegisterType(executingType)
                    .As(dependencyType)
                    .PropertiesAutowired()
                    .InstancePerDependency();
            }
            else
            {
                executingType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(s => s.GetInterfaces().Contains(dependencyType) && !s.IsAbstract);

                builder.RegisterType(executingType)
                         .As(dependencyType)
                          .PropertiesAutowired()
                          .InstancePerDependency();
            }
        }


        private static void RegisterRepositories(this ContainerBuilder builder,Type dependencyType, Assembly[] assemblies)
        {
            
            var currentTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.IsInterface && x.GetInterfaces().Any(p => p.Name == dependencyType.Name));

            foreach (var type in currentTypes)
            {
                var executingType = GetType(type,assemblies);
                if (executingType != null)
                {
                    builder.RegisterType(executingType)
                        .As(type).PropertiesAutowired()
                        .InstancePerLifetimeScope();
                }
                else
                {
                    var libraryType = Assembly.GetExecutingAssembly().GetTypes()
                        .FirstOrDefault(s => s.GetInterfaces().Contains(type));

                    builder.RegisterType(libraryType)
                       .As(type).PropertiesAutowired()
                       .InstancePerLifetimeScope();
                }
            }
        }

        private static Type GetType(Type type,Assembly[] assemblies)
        {
            Type executingType = null;
            foreach (var assembly in assemblies)
            {
                executingType = assembly.GetTypes()
                    .FirstOrDefault(s => s.GetInterfaces().Contains(type) && !s.IsInterface && !s.IsAbstract);

                if (executingType != null)
                    break;
            }
            return executingType;
        }

        private static Type FilterHandlers(Type type, Type handlerType)
        {
            return type
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType)
                .Single();
        }
    }
}
