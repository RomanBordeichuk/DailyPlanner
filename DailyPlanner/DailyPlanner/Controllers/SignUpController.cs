using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Models;

namespace DailyPlanner.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("User has been saved");

                return Redirect("Main");
            }

            return View();
        }
    }
}