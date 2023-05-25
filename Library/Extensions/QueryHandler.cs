

using System.Net;

namespace TasksLibrary.Extensions
{
    public abstract class QueryHandler<TQuery, TQueryContext, TResponse> : QueryHandler<TQueryContext>, IQueryHandler<TQuery, TResponse>
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TQuery command);

        protected ActionResult<TResponse> FailedOperation(string error)
        {
            return ActionResult<TResponse>.Failed(error);
        }

        protected ActionResult<TResponse> FailedOperation(string error, HttpStatusCode code)
        {
            return ActionResult<TResponse>.Failed(error, (int)code);
        }
        protected ActionResult<TResponse> SuccessfulOperation(TResponse response)
        {
            return ActionResult<TResponse>.SuccessfulOperation(response);
        }

    }
    public interface IQueryHandler<TQuery, TResponse>
    {
        Task<ActionResult<TResponse>> HandleAsync(TQuery command);
    }

    public abstract class QueryHandler<TQueryContext>
    {
        public TQueryContext QueryContext { get; set; }
    }


}
