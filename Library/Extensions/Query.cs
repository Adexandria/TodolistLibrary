

namespace TasksLibrary.Extensions
{
    public abstract class Query<TResponse> : IQuery<TResponse>
    {
        public abstract ActionResult Validate();
    }

    public interface IQuery<TResponse> : IRequestValidator
    {

    }

}
