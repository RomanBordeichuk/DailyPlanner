using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
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
            dailyTasksModel.DailyTasksRepository = _dailyTasksRepository;

            await dailyTasksModel.GetFromDbDailyTasksModel();

            return View(dailyTasksModel);
        }

        public async Task<IActionResult> ChangeDate(DailyTasksModel dailyTasksModel)
        {
            dailyTasksModel.DailyTasksRepository = _dailyTasksRepository;

            if (dailyTasksModel.DateChanged())
            {
                dailyTasksModel.ChangeGlobalDate();

                await dailyTasksModel.GetFromDbDailyTasksModel();

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
                if (!dailyTasksModel.DateChanged())
                {
                    dailyTasksModel.DailyTasksRepository = _dailyTasksRepository;

                    await dailyTasksModel.GetFromDbDailyTasksList();
                    await dailyTasksModel.SaveDailyTasksToDb();
                    await dailyTasksModel.GetFromDbDailyTasks();

                    dailyTasksModel.ErrorsMessagesList = new();
                }

                dailyTasksModel.ErrorsMessagesList.Add(
                    "Cannot save changes to another date");
            }

            return Redirect("Index");
        }

        public async Task<IActionResult> DiscardChanges(DailyTasksModel dailyTasksModel)
        {
            dailyTasksModel.DailyTasksRepository = _dailyTasksRepository;

            if (!dailyTasksModel.DateChanged() && dailyTasksModel.CorrectInputData)
            {
                await dailyTasksModel.GetFromDbDailyTasksModel();
            }
            else
            {
                dailyTasksModel.BackUpToGlobalDate();
            }

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
