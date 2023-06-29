using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Models
{
    public class UserViewModel : IUserViewModel
    {
        public string Login { get; set; } = null!;

        public UserViewModel(string login)
        {
            Login = login;
        }
        public UserViewModel(UserModel user)
        {
            Login = user.Login;
        }
        public UserViewModel(UserEntity user)
        {
            Login = user.Login;
        }
    }
}
