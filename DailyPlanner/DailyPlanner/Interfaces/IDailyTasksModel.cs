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
        bool CorrectInputData { get; set; }
        List<string> ErrorsMessagesList { get; set; }
        IDailyTasksRepository? DailyTasksRepository { get; set; }

        int NumTasks { get; }
        string DateString { get; set; }

        Task<DailyTasksModel> GetFromDbDailyTasksModel(); 
        Task<DailyTasksListEntity> GetFromDbDailyTasksList();
        Task<List<DailyTaskEntity>> GetFromDbDailyTasks();
        Task<List<DailyTaskEntity>> SaveDailyTasksToDb();
        void ChangeGlobalDate();
        void BackUpToGlobalDate();
        void SetDailyTasksListsToEachDailyTaskObj();
        bool DateChanged();
    }
}
