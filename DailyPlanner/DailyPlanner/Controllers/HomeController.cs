using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.Models;

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
                userModel.UserRepository = _userRepository;

                if (await userModel.ContainsUserInDb())
                {
                    await userModel.SetUserToStaticClass();
                    return Redirect("Main");
                }

                return View(userModel);
            }

            return View();
        }
    }
}
