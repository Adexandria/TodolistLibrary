

namespace TasksLibrary.Utilities
{
    public abstract class Command
    {
        public abstract ActionResult Validate();
    }

    public abstract class Command<T> : Command, ICommand<T>
    {

    }

    public interface ICommand<T> : IValidator 
    {
    }

}
