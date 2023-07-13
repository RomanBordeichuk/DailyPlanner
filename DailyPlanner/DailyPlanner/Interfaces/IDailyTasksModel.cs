using DailyPlanner.Models;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Interfaces
{
    public interface IDailyTasksModel : IModel
    {
        DateOnly Date { get; set; }
        List<DailyTaskEntity> DailyTasks { get; set; } 
        DailyTasksListEntity DailyTasksList { get; set; }
        int NumTasks { get; }
        List<string> ErrorsMessagesList { get; set; }

        string DateString { get; set; }
        bool CorrectInputData { get; set; }

        Task<DailyTasksModel> GetFromDbDailyTasksModel(
            IDailyTasksRepository _dailyTasksRepository); 
        Task<DailyTasksListEntity> GetFromDbDailyTasksList(
            IDailyTasksRepository _dailyTasksRepository);
        Task<List<DailyTaskEntity>> GetFromDbDailyTasks(
            IDailyTasksRepository _dailyTasksRepository);
        Task<List<DailyTaskEntity>> SaveDailyTasksToDb(
            IDailyTasksRepository _dailyTasksRepository);
        void SetDailyTasksListsToEachDailyTaskObj();
    }
}
