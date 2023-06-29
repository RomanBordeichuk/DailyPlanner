using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;

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
        public async Task<IActionResult> Index(UserModel user)
        {
            if (ModelState.IsValid)
            {
                if(await _userRepository.ContainsAsync(user))
                {
                    ViewData["ContainsUser"] = true;
                    return View();
                }

                ViewData["ContainsUser"] = false;
                await _userRepository.AddAsync(user);
                return Redirect("Main");
            }

            return View();
        }
    }
}