using DailyPlanner.Models;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class DailyTasksController : Controller
    {
        private readonly IDailyTasksRepository _dailyTasksRepository;

        public DailyTasksController(
            IDailyTasksRepository dailyTasksRepository)
        {
            _dailyTasksRepository = dailyTasksRepository;
        }

        public async Task<IActionResult> Index(DailyTasksModel dailyTasksModel)
        {
            await dailyTasksModel.GetFromDbDailyTasksModel(_dailyTasksRepository);

            return View(dailyTasksModel);
        }

        public async Task<IActionResult> ChangeDate(DailyTasksModel dailyTasksModel)
        {
            if (dailyTasksModel.DateChanged())
            {
                dailyTasksModel.ChangeGlobalDate();

                await dailyTasksModel.GetFromDbDailyTasksModel(_dailyTasksRepository);

                return Redirect("Index");
            }

            if (dailyTasksModel.CorrectInputData)
            {
                dailyTasksModel.ErrorsMessagesList.Add("Date hadn't changed");
            }

            return View("Index", dailyTasksModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTasks(DailyTasksModel dailyTasksModel)
        {
            if (ModelState.IsValid && dailyTasksModel.CorrectInputData)
            {
                await dailyTasksModel.GetFromDbDailyTasksList(_dailyTasksRepository);

                dailyTasksModel.SetDailyTasksListsToEachDailyTaskObj();

                await dailyTasksModel.SaveDailyTasksToDb(_dailyTasksRepository);
                await dailyTasksModel.GetFromDbDailyTasks(_dailyTasksRepository);
            }

            return View("Index", dailyTasksModel);
        }

        public async Task<IActionResult> DiscardChanges(DailyTasksModel dailyTasksModel)
        {
            if (!dailyTasksModel.DateChanged() && dailyTasksModel.CorrectInputData)
            {
                await dailyTasksModel.GetFromDbDailyTasksModel(_dailyTasksRepository);
            }

            dailyTasksModel.BackUpToGlobalDate();

            return Redirect("Index");
        }

        public IActionResult AddNewColumn(DailyTasksModel dailyTasksModel)
        {
            dailyTasksModel.DailyTasks.Add(new());

            return View("Index", dailyTasksModel);
        } 
        public IActionResult DropLatestColumn(DailyTasksModel dailyTasksModel)
        {
            if(dailyTasksModel.NumTasks > 0)
            {
                dailyTasksModel.DailyTasks.RemoveAt(dailyTasksModel.NumTasks - 1);
            }
            else
            {
                dailyTasksModel.ErrorsMessagesList.Add("There are no task field to delete");
            }

            return View("Index", dailyTasksModel);
        }
    }
}
