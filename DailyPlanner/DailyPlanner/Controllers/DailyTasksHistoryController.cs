using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class DailyTasksHistoryController : Controller
    {
        private readonly IDailyTasksRepository _dailyTasksRepository;

        public DailyTasksHistoryController(IDailyTasksRepository dailyTasksRepository)
        {
            _dailyTasksRepository = dailyTasksRepository;
        }
        
        public async Task<IActionResult> Index(
            DailyTasksHistoryModel dailyTasksHistoryModel)
        {
            dailyTasksHistoryModel.DailyTasksRepository = _dailyTasksRepository;

            await dailyTasksHistoryModel.GetFromDbDatesList();

            return View(dailyTasksHistoryModel);
        }

        public async Task<IActionResult> CurrentDailyTasksHistory(
            DailyTasksHistoryModel dailyTasksHistoryModel)
        {
            DateOnly test = ChosenDateStatic.ChosenDate;
            dailyTasksHistoryModel.DailyTasksRepository = _dailyTasksRepository;

            await dailyTasksHistoryModel.GetFromDbCurrentDailyTasksList();
            await dailyTasksHistoryModel.GetFromDbCurrentDailyTasks();

            return View(dailyTasksHistoryModel);
        }
    }
}
