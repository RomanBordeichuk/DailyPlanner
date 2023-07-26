using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository
    {
        Task<UserEntity> AddAsync(UserEntity user);
        Task<bool> ContainsAsync(UserEntity user);
        Task<UserEntity> GetAsync(UserEntity user);
        Task<string> GetMotivationalQuoteByUserId(int userId);
        Task<string> UpdateQuoteById(int userId, string quote);
    }
}
