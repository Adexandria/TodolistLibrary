using NHibernate;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services.Architecture.TaskModule
{
    public class AccountManagementContext : DatabaseManagementContext<AccessRepository>
    {
        public AccountManagementContext(AccessRepository context,ISession session,
            IUserRepository userRepository,
            IAuthToken authTokenRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IAccessTokenRepository accessTokenRepository ) : base(session)
        {
            Context = context;
            Context.UserRepository = userRepository;
            Context.AuthenTokenRepository = authTokenRepository;
            context.RefreshTokenRepository = refreshTokenRepository;
            Context.AccessTokenRepository = accessTokenRepository;
        }
       
    }
}
