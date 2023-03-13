using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksLibrary.Models.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetExistingUserById(Guid userId);
        Task<User> GetExistingUserByEmail(string email);
        Task<bool> IsExist(string email);
        void Add(User user);
        void Update(User user);
        Task<bool> CommitAsync();
        User AuthenticateUser(string email, string password);

    }
}
