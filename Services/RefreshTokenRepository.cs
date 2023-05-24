using TasksLibrary.Models;
using TasksLibrary.Models.Interfaces;
using TasksLibrary.NHibernate;

namespace TasksLibrary.Services
{
    public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(SessionFactory session) : base(session)
        {
        }
    }
}
