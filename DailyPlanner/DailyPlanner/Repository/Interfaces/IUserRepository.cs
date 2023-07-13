using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IUserRepository : 
        IBaseRepository<UserEntity>
    {
        Task<bool> ContainsAsync(UserEntity user);
        Task<UserEntity> GetAsync(UserEntity user);
    }
}
