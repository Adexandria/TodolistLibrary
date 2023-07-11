using Autofac;
using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Application
{
    public interface ITaskApplication
    {
        Task<ActionResult<TResponse>> ExecuteCommand<TCommand,TResponse>(IContainer container,TCommand command) 
            where TCommand : Command<TResponse>;

        Task<ActionResult> ExecuteCommand<TCommand>(IContainer container, TCommand command)
           where TCommand : Command;

        Task<ActionResult<TResponse>> SendQuery<TQuery,TResponse>(IContainer container,TQuery query)
            where TQuery : Query<TResponse>;
    }
}
