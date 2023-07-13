using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
