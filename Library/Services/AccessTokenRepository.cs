using NHibernate;
using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using NHibernate.Linq;

namespace TasksLibrary.Services
{
    public class AccessTokenRepository : Repository<IAccessToken>,IAccessTokenRepository
    {
        public AccessTokenRepository(ISession session) : base(session)
        {
        }

        public async Task Delete(Guid entityId)
        {
            var entity = await Session.Query<AccessToken>().FirstOrDefaultAsync(s => s.Id == entityId);
            if (entity != null)
            {
                await Delete(entity);
            }
        }
    }
}
