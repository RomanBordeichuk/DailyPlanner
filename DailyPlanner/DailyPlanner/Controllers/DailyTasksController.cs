using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class DailyTasksController : Controller
    {
        private readonly IDailyTaskRepository _dailyTaskRepository;

        public DailyTasksController(
            IDailyTaskRepository dailyTaskRepository)
        {
            _dailyTaskRepository = dailyTaskRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(DailyTaskModel dailyTask)
        {
            if (ModelState.IsValid)
            {
                await _dailyTaskRepository.AddAsync(dailyTask);

                return View();
            }

            return View();
        }
    }
}
