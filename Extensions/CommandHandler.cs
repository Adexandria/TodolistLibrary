using System.Net;
using TasksLibrary.Services.Architecture.Database;

namespace TasksLibrary.Extensions
{

    public abstract class CommandHandler<TCommand, TDbcontext, TResponse> : CommandHandler<TDbcontext>, ICommandHandler<TCommand, TDbcontext,TResponse>
        where TCommand : Command<TResponse>
        where TDbcontext : class, IDbContext
    {

        public abstract Task<ActionResult<TResponse>> HandleCommand(TCommand command);
        protected  ActionResult<TResponse> FailedOperation(string error)
        {
            return ActionResult<TResponse>.Failed(error);
        }

        protected  ActionResult<TResponse> FailedOperation(string error, HttpStatusCode code)
        {
            return ActionResult<TResponse>.Failed(error,(int)code);
        }
        protected ActionResult<TResponse> SuccessfulOperation(TResponse response)
        {
            return ActionResult<TResponse>.SuccessfulOperation(response);
        }
    }

    public abstract class CommandHandler<TCommand,TDbcontext> : CommandHandler<TDbcontext> , ICommandHandler<TCommand,TDbcontext>
        where TCommand : Command
        where TDbcontext : class,IDbContext
    {

        public abstract Task<ActionResult> HandleCommand(TCommand command);

        protected ActionResult FailedOperation(string error)
        {
            return ActionResult.Failed(error);
        }

        protected ActionResult FailedOperation(string error, HttpStatusCode code)
        {
            return ActionResult.Failed(error, ((int)code));
        }

        protected ActionResult SuccessfulOperation()
        {
            return ActionResult.Successful();
        }
    }

    public interface ICommandHandler<TCommand,TDbcontext>
    {
        public abstract Task<ActionResult> HandleCommand(TCommand command);
    }
    public interface ICommandHandler<TCommand,TDbcontext,TResponse>
    {
        public abstract Task<ActionResult<TResponse>> HandleCommand(TCommand command);
    }

    public abstract class CommandHandler<TDbcontext>
    {
        public TDbcontext Dbcontext;
    }

}

