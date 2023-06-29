using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Entitites;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Models
{
    public class UserModel : IUserModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Login must have at least 3 letters")]
        [MaxLength(30, ErrorMessage = "Login must have not more than 30 letters")]
        public string Login { get; set; } = null!;

        [Required]
        [MinLength(5, ErrorMessage = "Password must have at least 5 letters")]
        [MaxLength(30, ErrorMessage = "Login must have not more than 30 letters")]
        public string Password { get; set; } = null!;
    }
}
