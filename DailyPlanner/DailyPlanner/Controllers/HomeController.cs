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
        public async Task<IActionResult> Index(User user)
        {
            if (ModelState.IsValid)
            {
                if(await _userRepository.ContainsAsync(user))
                {
                    ViewData["ContainsUser"] = true;
                    return Redirect("Main");
                }

                ViewData["ContainsUser"] = false;
                return View();
            }

            return View();
        }
    }
}
