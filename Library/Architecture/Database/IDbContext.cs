using TasksLibrary.Utilities;
namespace TasksLibrary.Architecture.Database
{
    public interface IDbContext
    {
        Task<ActionResult> CommitAsync();
    }
}