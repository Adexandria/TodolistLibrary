using TasksLibrary.Services;

namespace TasksLibrary.Models.Interfaces
{
    public interface IAccessTokenRepository : IRepository<IAccessToken>
    {
        Task Delete(Guid entityId);
    }
}
