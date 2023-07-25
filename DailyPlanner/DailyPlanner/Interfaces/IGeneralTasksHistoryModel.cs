using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Interfaces
{
    public interface IGeneralTasksHistoryModel : IModel
    {
        List<(int, int)> MonthDatesList { get; set; }
        (int, int) CurrentMonthNum { get; set; }
        List<GeneralTaskEntity> CurrentMonthTasksList { get; set; }
        bool CorrectInputData { get; set; }
        List<string> ErrorMessagesList { get; set; }
        IGeneralTasksRepository? GeneralTasksRepository { get; set; }

        int NumMonthDates { get; }
        int NumCurrentMonthTasks { get; }
        string CurrentMonthString { get; set; }

        string GetMonthName(int monthNumber);
        Task<List<(int, int)>> GetFromDbMonthDatesList();
        Task<List<GeneralTaskEntity>> GetFromDbCurrentMonthGeneralTasks();
        Task<bool> SaveChangesWithGeneralTasks();
    }
}
