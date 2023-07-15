using DailyPlanner.Models;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Interfaces
{
    public interface IGeneralTasksModel : IModel
    {
        DateOnly Date { get; set; }
        List<GeneralTaskEntity> GeneralTasks { get; set; }
        bool CorrectInputData { get; set; }
        List<string> ErrorMessagesList { get; set; }
        IGeneralTasksRepository? GeneralTasksRepository { get; set; }

        int NumTasks { get; }
        string DateString { get; set; }

        Task<GeneralTasksModel> GetFromDbGeneralTasks();
        Task<bool> SaveGeneralTasksToDb();
        void SetUserEntityIdToEachGeneralTask();
        bool CorrectGeneralTasksDates();
    }
}
