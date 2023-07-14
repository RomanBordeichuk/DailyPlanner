using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.Models;

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
                userModel.UserRepository = _userRepository;

                if (await userModel.ContainsUserInDb())
                {
                    return View(userModel);
                }

                await userModel.AddUserToDb();
                await userModel.SetUserToStaticClass();

                return Redirect("Main");
            }

            return View();
        }
    }
}