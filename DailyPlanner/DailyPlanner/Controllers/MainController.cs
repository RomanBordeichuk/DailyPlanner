using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyPlanner.Controllers
{
    public class MainController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IDailyTasksRepository _dailyTasksRepository;
        private readonly IGeneralTasksRepository _generalTasksRepository;

        public MainController(IUserRepository userRepository,
            IDailyTasksRepository dailyTasksRepository, 
            IGeneralTasksRepository generalTasksRepository)
        {
            _userRepository = userRepository;
            _dailyTasksRepository = dailyTasksRepository;
            _generalTasksRepository = generalTasksRepository;
        }

        public async Task<IActionResult> Index(MainModel mainModel)
        {
            mainModel.UserRepository = _userRepository;
            mainModel.DailyTasksRepository = _dailyTasksRepository;
            mainModel.GeneralTasksRepository = _generalTasksRepository;

            await mainModel.GetFromDbMotivationalQuote();
            await mainModel.GetFromDbClosestTask();

            return View(mainModel);
        }

        public async Task<IActionResult> SaveChanges(MainModel mainModel)
        {
            mainModel.UserRepository = _userRepository;
            mainModel.DailyTasksRepository = _dailyTasksRepository;
            mainModel.GeneralTasksRepository = _generalTasksRepository;

            await mainModel.SaveChangedMotivationalQuote(); 

            return View("Index", mainModel);
        }
    }
}
