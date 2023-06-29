using DailyPlanner.Models;
using DailyPlanner.Repository.Hashing;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Repository.Entitites
{
    public class UserEntity : IUserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;

        public UserEntity(string login, string password)
        {
            Login = login;
            Password = password;
        }
        public UserEntity(UserModel user)
        {
            Login = user.Login;
            Password = PasswordHashing.HashPassword(user.Password);
        }
    }
}
