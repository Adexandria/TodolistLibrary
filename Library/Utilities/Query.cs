

namespace TasksLibrary.Utilities
{
    public abstract class Query<TResponse> : IQuery<TResponse>
    {
        public abstract ActionResult Validate();
    }

    public interface IQuery<TResponse> : IQuery
    {

    }

    public interface IQuery : IValidator { }


}
