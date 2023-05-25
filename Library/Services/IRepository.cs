


namespace TasksLibrary.Services
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetAll();
        Task<T> GetExistingEntityById(Guid id);

    }
}
