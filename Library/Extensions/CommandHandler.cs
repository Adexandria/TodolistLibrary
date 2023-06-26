using System.Net;
using TasksLibrary.Architecture.Database;

namespace TasksLibrary.Extensions
{

    public abstract class CommandHandler<TCommand, TDbcontext, TResponse> : CommandHandler<TDbcontext>, ICommandHandler<TCommand,TResponse>
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

        protected ActionResult<TResponse> SuccessfulOperation(TResponse response,string accessToken, string refreshToken)
        {
            return ActionTokenResult<TResponse>.SuccessfulOperation(response,accessToken,refreshToken);
        }
    }

    public abstract class CommandHandler<TCommand,TDbcontext> : CommandHandler<TDbcontext> , ICommandHandler<TCommand>
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

    public interface ICommandHandler<TCommand>
    {
        public Task<ActionResult> HandleCommand(TCommand command);
    }
    public interface ICommandHandler<TCommand,TResponse>
    {
        public Task<ActionResult<TResponse>> HandleCommand(TCommand command);
    }

    public abstract class CommandHandler<TDbcontext>
    {
        public TDbcontext Dbcontext { get; set; }
    }

}

