using NHibernate;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services.Architecture.TaskModule
{
    public class AccountManagementContext : DatabaseManagementContext<AccessManagement>
    {
        public AccountManagementContext(AccessManagement context,ISession session,
            IUserRepository userRepository,
            IAuthToken authTokenRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IAccessTokenRepository accessTokenRepository ) : base(session)
        {
            Context = context;
            Context.UserRepository = userRepository;
            Context.AuthenTokenRepository = authTokenRepository;
            Context.RefreshTokenRepository = refreshTokenRepository;
            Context.AccessTokenRepository = accessTokenRepository;
        }
       
    }
}
