using NHibernate;
using NHibernate.Linq;
using TasksLibrary.Client.Model;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.Services;

namespace TasksLibrary.Client.Services
{
    public class UserService : Repository<IUser>, IUserRepository
    {
        public UserService(ISession session) : base(session)
        {
        }

        public Task<IUser> AuthenticateUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IUser> GetExistingUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsExist(string email)
        {
            var user = await Session.Query<IUser>().FirstOrDefaultAsync(s => s.Email == email);
            return user != null;
        }
    }
}
