using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class GeneralTasksController : Controller
    {
        private readonly IGeneralTasksRepository _generalTasksRepository;

        public GeneralTasksController(IGeneralTasksRepository generalTasksRepository)
        {
            _generalTasksRepository = generalTasksRepository;
        }

        public async Task<IActionResult> Index(GeneralTasksModel generalTasksModel)
        {
            generalTasksModel.GeneralTasksRepository = _generalTasksRepository;

            await generalTasksModel.GetFromDbGeneralTasks();

            return View(generalTasksModel);
        }
        public async Task<IActionResult> SaveTasks(GeneralTasksModel generalTasksModel)
        { 
            if (ModelState.IsValid && generalTasksModel.CorrectInputData)
            {
                generalTasksModel.GeneralTasksRepository = _generalTasksRepository;

                if (generalTasksModel.CorrectGeneralTasksDates())
                {
                    await generalTasksModel.SaveGeneralTasksToDb();
                    await generalTasksModel.GetFromDbGeneralTasks();

                    generalTasksModel.ErrorMessagesList = new();

                    return Redirect("Index");
                }

                generalTasksModel.ErrorMessagesList.Add("Some tasks dates are incorrect");
            }

            return View("Index", generalTasksModel);
        }
        public IActionResult DiscardChanges()
        {
            return Redirect("Index");
        }
        public IActionResult AddColumn(GeneralTasksModel generalTasksModel)
        {
            generalTasksModel.GeneralTasks.Add(new());

            return View("Index", generalTasksModel);
        }
        public IActionResult DeleteColumn(GeneralTasksModel generalTasksModel)
        {
            if(generalTasksModel.NumTasks > 0)
            {
                generalTasksModel.GeneralTasks.RemoveAt(generalTasksModel.NumTasks - 1);
            }
            else
            {
                generalTasksModel.ErrorMessagesList.Add(
                    "There are no task fields to delete");
            }

            return View("Index", generalTasksModel);
        }
    }
}
