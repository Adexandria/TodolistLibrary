


namespace TasksLibrary.Services
{
    public interface IRepository<T>
    {
        Task Add(T user);
        Task Update(T user);
        Task Delete(T user);
        Task<List<T>> GetAll();

    }
}
