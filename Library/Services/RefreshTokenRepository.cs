using NHibernate;
using NHibernate.Linq;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;

namespace TasksLibrary.Services
{
    public class RefreshTokenRepository : Repository<IRefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ISession session) : base(session)
        {
        }

        public async Task<UserId> GetUserByRefreshToken(string refreshToken)
        {
            return await Session.Query<RefreshToken>().Where(s => s.Token.Equals(refreshToken))
                .Select(s => s.UserId).FirstOrDefaultAsync();
        }
    }
}
