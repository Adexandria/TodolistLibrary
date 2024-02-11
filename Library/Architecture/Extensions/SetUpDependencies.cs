using Autofac;
using System.Reflection;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Architecture.Database;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Extensions
{
    public static class SetUpDependencies
    {
        public static ContainerBuilder UseDefaultDependencies(this ContainerBuilder builder)
        {
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

            builder.RegisterType<PasswordManager>()
                .As<IPasswordManager>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryContext<Note>>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            //Specify the types to register commandhandler,query, query handler and commands
            builder.RegisterAssemblyTypes(typeof(CreateUserCommandHandler).Assembly)
               .AsImplementedInterfaces()
               .PropertiesAutowired()
               .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder UseCustomDependencies(this ContainerBuilder builder, string[] assemblies)
        {
            builder.Register(typeof(IRepository<>), assemblies);

            builder.Register(typeof(Command), assemblies);

            builder.Register(typeof(Command<>), assemblies);

            builder.Register(typeof(IAuthToken), assemblies);

            builder.Register(typeof(Query<>),assemblies);

            return builder;
        }

        private static void Register(this ContainerBuilder builder, Type dependencyType, string[] assemblies)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var currentTypes = Assembly.GetExecutingAssembly().GetTypes().Where(s => s.BaseType?.GetInterfaces()[0] == dependencyType);

            foreach (var type in currentTypes)
            {
                var executingType = GetExecutingType(type, assemblies);
                if (executingType != null)
                {
                    builder.Register((_) => executingType)
                        .As(type).PropertiesAutowired()
                        .InstancePerLifetimeScope();
                }
                else
                {
                    var libraryType = Assembly.GetExecutingAssembly().GetTypes()
                        .FirstOrDefault(s => s.BaseType == type);

                    builder.Register((_) => libraryType)
                       .As(type).PropertiesAutowired()
                       .InstancePerLifetimeScope();
                }
            }
        }

        private static Type GetExecutingType(Type type, string[] assemblies)
        {
            Type executingType = null;
            foreach (var assembly in assemblies)
            {
                var currentAssembly = Assembly.Load(assembly);
                executingType = currentAssembly.GetTypes().FirstOrDefault(s => s.BaseType == type);
                if (executingType != null)
                    break;
            }
            return executingType;
        }
    }
}
