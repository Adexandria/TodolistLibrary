using NHibernate;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services.Architecture.TaskModule
{
    public class AccountManagementContext : DatabaseManagementContext<AccessRepository>
    {
        public AccountManagementContext(ISession session,
            IUserRepository userRepository,
            IAuthToken authTokenRepository) : base(session)
        {
            Context.UserRepository = userRepository;
            Context.AuthenTokenRepository = authTokenRepository;
        }
       
    }
}
