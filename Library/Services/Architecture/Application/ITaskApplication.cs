using Autofac;
using TasksLibrary.Extensions;

namespace TasksLibrary.Services.Architecture.Application
{
    public interface ITaskApplication
    {
        Task<ActionResult<TResponse>> ExceuteCommand<TCommand,TResponse>(IContainer container,TCommand command) 
            where TCommand : Command<TResponse>;

        Task<ActionResult> ExceuteCommand<TCommand>(IContainer container, TCommand command)
           where TCommand : Command;

        Task<ActionResult<TResponse>> SendQuery<TQuery,TResponse>(IContainer container,TQuery query)
            where TQuery : Query<TResponse>;
    }
}
