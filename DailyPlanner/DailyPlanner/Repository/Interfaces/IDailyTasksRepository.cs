using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IDailyTasksRepository : IBaseRepository
    {
        Task<DailyTaskEntity> AddAsync(DailyTaskEntity dailyTask);
        Task<DailyTaskEntity> UpdateAsync(
            DailyTaskEntity oldDailyTask, DailyTaskEntity newDailyTask);
        Task<DailyTaskEntity> DeleteAsync(DailyTaskEntity dailyTask);
        Task<DailyTasksListEntity> GetDailyTasksListObjByDate(DateOnly date);
        Task<DailyTasksListEntity> DeleteDailyTasksList(
            DailyTasksListEntity dailyTasksList);
        Task<List<DailyTaskEntity>> GetDailyTasksById(int id);
        Task<List<DateOnly>> GetDailyTasksDays();
        Task<List<DailyTaskEntity>> GetDailyTasksByListId(int listId);
    }
}
