using TasksLibrary.Utilities;

namespace TasksLibrary.Architecture.Database
{
    public abstract class DbContext<T> : IDbContext
    {
        public virtual T Context { get; set; }
        public abstract Task<ActionResult> CommitAsync();
    }
}
