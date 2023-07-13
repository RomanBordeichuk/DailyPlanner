using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class GeneralTasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
