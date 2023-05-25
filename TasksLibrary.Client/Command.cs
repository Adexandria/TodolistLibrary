using Autofac;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Application.Commands.Login;
using TasksLibrary.Services.Architecture.Application;

namespace TasksLibrary.Client
{
    public class Command
    {
        public Command(IContainer container,ITaskApplication application)
        {
            Container = container;
            Application = application;
        }
        public async Task<dynamic> CreateUser(CreateUserCommand command)
        {
            var response = await Application.ExceuteCommand<CreateUserCommand, CreateUserDTO>(Container, command);
            if (response.NotSuccessful)
                return response.Errors[0];
            return response.Data;
        }

        public  async Task<dynamic> LoginUser(LoginCommand command)
        {
            var response = await Application.ExceuteCommand<LoginCommand, LoginDTO>(Container, command);
            if (response.NotSuccessful)
                return response.Errors[0];
            return response;
        }
        public IContainer Container;
        public ITaskApplication Application;
    }
}
