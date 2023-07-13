using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.StaticClasses;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Models
{
    public class UserModel : IHomePageModel
    {
        [Required]
        [MinLength(3, ErrorMessage = 
            "Login must have at least 3 letters")]
        public string UserLogin { get; set; } = null!;

        [Required]
        [MinLength(6, ErrorMessage =
            "Password must have at least 6 letters")]
        public string UserPassword { get; set; } = null!;

        public bool HasUserInDb { get; set; }

        public void SetUser(UserEntity user)
        {
            CurrentUserStatic.User = user;
        }
    }
}
