using DailyPlanner.Models;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IUserRepository : 
        IBaseRepository<UserModel, UserViewModel>
    {
        Task<bool> ContainsAsync(UserModel user);
    }
}
