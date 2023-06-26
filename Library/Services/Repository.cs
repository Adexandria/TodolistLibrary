using NHibernate;
using NHibernate.Linq;

namespace TasksLibrary.Services
{
    public class Repository<T>
    {
        public ISession Session;
        public Repository(ISession session)
        {
            Session = session;
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
        public async Task<T> GetExistingEntityById(Guid id)
        {
            var entity = await Session.GetAsync<T>(id);
            return entity;
        }
    }
}
