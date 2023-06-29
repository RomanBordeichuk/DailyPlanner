using DailyPlanner.Models;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IUserRepository : 
        IBaseRepository<User>
    {
        Task<bool> ContainsAsync(User user);
    }
}
