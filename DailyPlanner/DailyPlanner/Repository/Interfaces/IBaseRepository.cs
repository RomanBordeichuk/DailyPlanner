namespace DailyPlanner.Repository.Interfaces
{
    public interface IBaseRepository<T, K>
    {
        Task<K> AddAsync(T entity);
        Task<K> GetByIdAsync(int id);
        Task<List<K>> GetAllAsync();
        Task<K> UpdateByIdAsync(int id, T entity);
        Task<K> DeleteByIdAsync(int id);
    }
}
