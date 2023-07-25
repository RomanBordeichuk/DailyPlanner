using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Hashing;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Models
{
    public class UserModel
    {
        [Required]
        [MinLength(3, ErrorMessage = 
            "Login must have at least 3 letters")]
        public string UserLogin { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage =
            "Password must have at least 6 letters")]
        public string UserPassword { get; set; } = null!;
        public UserEntity? UserEntity { get; set; }

        public bool HasUserInDb { get; set; } = false;
        public IUserRepository? UserRepository { get; set; }

        public async Task<bool> ContainsUserInDb()
        {
            UserEntity = new(
                UserLogin,
                PasswordHashing.HashPassword(UserPassword));

            if(UserRepository != null)
            {
                if (await UserRepository.ContainsAsync(UserEntity))
                {
                    HasUserInDb = true;
                    return true;
                }

                return false;
            }

            throw new Exception("Repository not found");
        }

        public async Task<UserEntity> AddUserToDb()
        {
            if (UserRepository != null)
            {
                if(UserEntity != null)
                {
                    await UserRepository.AddAsync(UserEntity);

                    return UserEntity;
                }

                throw new Exception("UserEntity not found");
            }

            throw new Exception("Repository not found");
        }

        public async Task<UserEntity> SetUserToStaticClass()
        {
            if(UserRepository != null)
            {
                if(UserEntity != null)
                {
                    CurrentUserStatic.User =
                        await UserRepository.GetAsync(UserEntity);

                    return UserEntity;
                }

                throw new Exception("UserEntity not found");
            }

            throw new Exception("Repository not found");
        }
    }
}
