using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Interfaces;

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
        public IActionResult Index(IUser user)
        {
            if (ModelState.IsValid)
            {
                

                return Redirect("Main");
            }

            return View();
        }
    }
}
