using NHibernate;
using NHibernate.Linq;
using TasksLibrary.NHibernate;

namespace TasksLibrary.Services
{
    public class Repository<T>
    {
        public ISession Session;
        public Repository(SessionFactory session)
        {
            Session = session.GetSession();
        }

        public async Task Add(T entity)
        {
            await Session.SaveOrUpdateAsync(entity);
        }

        public async Task Update(T entity)
        {
            await Session.UpdateAsync(entity);
        }

        public async Task Delete(T entity)
        {
            await Session.DeleteAsync(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await Session.Query<T>().ToListAsync();
        }
    }
}
