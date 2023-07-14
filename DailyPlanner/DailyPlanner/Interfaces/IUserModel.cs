using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Interfaces
{
    public interface IUserModel : IModel
    {
        string UserLogin { get; set; }
        string UserPassword { get; set; }
        UserEntity? UserEntity { get; set; }
        bool HasUserInDb { get; set; }
        IUserRepository? UserRepository { get; set; }

        Task<bool> ContainsUserInDb();
        Task<UserEntity> AddUserToDb();
        Task<UserEntity> SetUserToStaticClass();
    }
}
