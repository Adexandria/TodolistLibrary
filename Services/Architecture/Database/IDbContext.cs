using TasksLibrary.Extensions;

namespace TasksLibrary.Services.Architecture.Database
{
    public interface IDbContext
    {
        Task<ActionResult> CommitAsync();
    }
}