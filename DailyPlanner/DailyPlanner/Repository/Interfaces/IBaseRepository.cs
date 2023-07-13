namespace DailyPlanner.Repository.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> AddAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateByIdAsync(int id, T entity);
        Task<T> DeleteByIdAsync(int id);
    }
}
