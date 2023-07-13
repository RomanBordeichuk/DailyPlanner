using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Hashing;

namespace DailyPlanner.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IUserRepository _userRepository; 

        public SignUpController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                UserEntity userEntity = new(
                    userModel.UserLogin, 
                    PasswordHashing.HashPassword(userModel.UserPassword));

                if(await _userRepository.ContainsAsync(userEntity))
                {
                    userModel.HasUserInDb = true;
                    return View(userModel);
                }

                userModel.HasUserInDb = false;

                await _userRepository.AddAsync(userEntity);
                userModel.SetUser(await _userRepository.GetAsync(userEntity));

                return Redirect("Main");
            }

            return View();
        }
    }
}