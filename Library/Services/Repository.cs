using NHibernate;
using NHibernate.Linq;
using TasksLibrary.Models;

namespace TasksLibrary.Services
{
    public class Repository<T> where T: IEntity
    {
        public ISession Session;
        public Repository(ISession session)
        {
            Session = session;
        }

        public virtual async Task Add(T entity)
        {
            await Session.SaveOrUpdateAsync(entity);
        }

        public virtual async Task Update(T entity)
        {
            await Session.UpdateAsync(entity);
        }

        public virtual async Task Delete(T entity)
        {
            await Session.DeleteAsync(entity);
        }

        public virtual async Task<List<T>> GetAll()
        {
            return await Session.Query<T>().ToListAsync();
        }
        public virtual async Task<T> GetExistingEntityById(Guid id)
        {
            var entity = await Session.GetAsync<T>(id);
            return entity;
        }
    }
}
