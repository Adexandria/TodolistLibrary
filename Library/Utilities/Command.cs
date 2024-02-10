

namespace TasksLibrary.Utilities
{
    public abstract class Command: IValidator
    {
        public abstract ActionResult Validate();
    }

    public abstract class Command<T> : Command
    {

    }
}
