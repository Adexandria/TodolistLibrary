using TasksLibrary.Services;

namespace TasksLibrary.Models.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetExistingUserByEmail(string email);
        Task<bool> IsExist(string email);
        Task<User> AuthenticateUser(string email, string password);

    }
}
