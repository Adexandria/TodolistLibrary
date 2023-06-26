using Autofac;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Application
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

        public async Task<ActionResult> ExceuteCommand<TCommand>(IContainer container, TCommand command) where TCommand : Command
        {
            var validationOutput = command.Validate();
            if (validationOutput.NotSuccessful)
            {
                return ActionResult.Failed().AddErrors(validationOutput.Errors);
            }
            var scope = container.BeginLifetimeScope();
            var handler = scope.Resolve<ICommandHandler<TCommand>>();
            try
            {
                return await handler.HandleCommand(command);

            }
            catch (Exception ex)
            {
                return ActionResult.Failed().AddError(ex.Message);

            }
        }

        public async Task<ActionResult<TResponse>> SendQuery<TQuery, TResponse>(IContainer container, TQuery query) where TQuery : Query<TResponse>
        {
            var validationOutput = query.Validate();
            if (validationOutput.NotSuccessful)
            {
                return ActionResult<TResponse>.Failed().AddErrors(validationOutput.Errors);
            }
            var scope = container.BeginLifetimeScope();
            var handler = scope.Resolve<IQueryHandler<TQuery,TResponse>>();
            try
            {
                return await handler.HandleAsync(query);

            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError(ex.Message);

            }
        }
    }
}
