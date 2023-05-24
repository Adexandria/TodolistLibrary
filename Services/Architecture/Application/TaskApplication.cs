using Autofac;
using TasksLibrary.Extensions;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services.Architecture.Database;

namespace TasksLibrary.Services.Architecture.Application
{
    public class TaskApplication : ITaskApplication
    {
        public async Task<ActionResult<TResponse>> ExceuteCommand<TCommand,TResponse>(IContainer container,TCommand command) 
            where TCommand : Command<TResponse>
        {
            var validationOutput = command.Validate();
            if (validationOutput.NotSuccessful)
            {
                return ActionResult<TResponse>.Failed().AddErrors(validationOutput.Errors);
            }
            var scope = container.BeginLifetimeScope();
            var handler = scope.Resolve<ICommandHandler<TCommand,TResponse>>();
            try
            {
                return  await handler.HandleCommand(command);
                
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError(ex.Message);
                
            }
        }
    }
}
