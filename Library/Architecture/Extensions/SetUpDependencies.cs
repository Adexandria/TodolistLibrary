using Autofac;
using System.Reflection;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;

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

            builder.RegisterAssemblyTypes(typeof(CreateUserCommandHandler).Assembly)
               .AsImplementedInterfaces()
               .PropertiesAutowired()
               .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder SetUpCustomDependencies(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            return builder;
        }
    }
}
