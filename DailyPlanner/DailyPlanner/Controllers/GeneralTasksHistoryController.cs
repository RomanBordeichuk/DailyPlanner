using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class GeneralTasksHistoryController : Controller
    {
        private readonly IGeneralTasksRepository _generalTasksRepository;

        public GeneralTasksHistoryController(
            IGeneralTasksRepository generalTasksRepository)
        {
            _generalTasksRepository = generalTasksRepository;
        }

        public async Task<IActionResult> Index(
            GeneralTasksHistoryModel generalTasksHistoryModel)
        {
            generalTasksHistoryModel.GeneralTasksRepository = _generalTasksRepository;

            await generalTasksHistoryModel.GetFromDbMonthDatesList();

            return View(generalTasksHistoryModel);
        }

        public async Task<IActionResult> MonthGeneralTasksHistory(
            GeneralTasksHistoryModel generalTasksHistoryModel)
        {
            generalTasksHistoryModel.GeneralTasksRepository = _generalTasksRepository;

            await generalTasksHistoryModel.GetFromDbCurrentMonthGeneralTasks();

            return View(generalTasksHistoryModel);
        }
        public async Task<IActionResult> SaveChanges(
            GeneralTasksHistoryModel generalTasksHistoryModel)
        {
            generalTasksHistoryModel.GeneralTasksRepository = _generalTasksRepository;

            generalTasksHistoryModel.CheckGeneralTasksInputData();

            if (ModelState.IsValid && generalTasksHistoryModel.CorrectInputData)
            {
                await generalTasksHistoryModel.SaveChangesWithGeneralTasks();
                await generalTasksHistoryModel.GetFromDbCurrentMonthGeneralTasks();
            }

            return View("MonthGeneralTasksHistory", generalTasksHistoryModel);
        }
        public IActionResult DiscardChanges(
            GeneralTasksHistoryModel generalTasksHistoryModel)
        {
            return Redirect("MonthGeneralTasksHistory");
        }
    }
}
