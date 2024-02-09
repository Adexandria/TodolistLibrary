using TasksLibrary.Services;

namespace TasksLibrary.Models.Interfaces
{
    public interface IUserRepository : IRepository<IUser>
    {
        Task<IUser> GetExistingUserByEmail(string email);
        Task<bool> IsExist(string email);
        Task<IUser> AuthenticateUser(string email, string password);

    }
}
