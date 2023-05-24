using NHibernate.Linq;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.NHibernate;

namespace TasksLibrary.Services
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(SessionFactory session) : base(session)
        {
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            var user = await Session.Query<User>().FirstOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                var isVerified = user.VerifyPassword(password, user.Salt);
                if (isVerified)
                {
                    return user;
                }
            }
            return default;
        }

        public async Task<User> GetExistingUserByEmail(string email)
        {
            var user = await Session.Query<User>().FirstOrDefaultAsync(s => s.Email == email);
            return user;
        }

        public async Task<User> GetExistingUserById(Guid userId)
        {
            var user = await Session.GetAsync<User>(userId);
            return user;
        }
        public async Task<bool> IsExist(string email)
        {
            var user = await Session.Query<User>().FirstOrDefaultAsync(s => s.Email == email);
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
