using Autofac;
using TasksLibrary.Application.Commands.CreateTask;
using TasksLibrary.Application.Commands.CreateUser;
using TasksLibrary.Application.Commands.DeleteTask;
using TasksLibrary.Application.Commands.GenerateToken;
using TasksLibrary.Application.Commands.Login;
using TasksLibrary.Application.Commands.UpdateTask;
using TasksLibrary.Application.Commands.VerifyToken;
using TasksLibrary.Architecture.Application;

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

        public async Task<dynamic> CreateTask(CreateTaskDTO newNote,string accessToken)
        {
            var accessCommand = new VerifyTokenCommand()
            { 
                AccessToken = accessToken
            };

            var response = await Application.ExceuteCommand<VerifyTokenCommand,string>(Container,accessCommand);
            if (response.NotSuccessful)
                return response.Errors[0];

            var taskCommand = new CreateTaskCommand()
            {
                Task = newNote.Task,
                Description = newNote.Description,
                UserId = new Guid(response.Data)
            };

            var createdResponse = await Application.ExceuteCommand<CreateTaskCommand,Guid>(Container,taskCommand);
            if (createdResponse.NotSuccessful)
                return createdResponse.Errors[0];

            return createdResponse.Data;

        }

        public async Task<dynamic> UpdateTask(UpdateTaskCommand command,string accessToken)
        {
            var accessCommand = new VerifyTokenCommand()
            {
                AccessToken = accessToken
            };

            var response = await Application.ExceuteCommand<VerifyTokenCommand, string>(Container, accessCommand);
            if (response.NotSuccessful)
                return response.Errors[0];

            var updatedResponse = await Application.ExceuteCommand(Container,command);
            if (updatedResponse.NotSuccessful)
                return updatedResponse.Errors[0];
            return updatedResponse;
        }

        public async Task<dynamic> DeleteTask(Guid taskId,string accessToken)
        {
            var accessCommand = new VerifyTokenCommand()
            {
                AccessToken = accessToken
            };

            var response = await Application.ExceuteCommand<VerifyTokenCommand, string>(Container, accessCommand);
            if (response.NotSuccessful)
                return response.Errors[0];

            var command = new DeleteTaskCommand()
            {
                TaskId = taskId
            };

            var deletedResponse = await Application.ExceuteCommand(Container, command);
            if (deletedResponse.NotSuccessful)
                 return deletedResponse.Errors[0];

            return deletedResponse;
        }

        public async Task<string> GetNewAcessToken(string refreshToken)
        {
            var command = new GenerateTokenCommand()
            {
                RefreshToken = refreshToken
            };
            var response = await Application.ExceuteCommand<GenerateTokenCommand,string>(Container, command);
            if (response.NotSuccessful)
                return response.Errors[0];
            return response.Data;
        }
        public IContainer Container;
        public ITaskApplication Application;
    }
}
