using TasksLibrary.Extensions;

namespace TasksLibrary.Architecture.Database
{
    public interface IDbContext
    {
        Task<ActionResult> CommitAsync();
    }
}