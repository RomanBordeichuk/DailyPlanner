using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.Models;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Hashing;

namespace DailyPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository)
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

                if (await _userRepository.ContainsAsync(userEntity))
                {
                    userModel.HasUserInDb = true;
                    userModel.SetUser(await _userRepository.GetAsync(userEntity));

                    return Redirect("Main");
                }

                userModel.HasUserInDb = false;
                return View(userModel);
            }

            return View();
        }
    }
}
