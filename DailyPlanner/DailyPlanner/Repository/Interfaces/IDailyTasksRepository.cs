using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IDailyTasksRepository :
        IBaseRepository<DailyTaskEntity>
    {
        Task<DailyTaskEntity> UpdateAsync(
            DailyTaskEntity oldDailyTask, DailyTaskEntity newDailyTask);
        Task<DailyTaskEntity> DeleteAsync(DailyTaskEntity dailyTask);
        Task<List<DailyTaskEntity>> AddListAsync(List<DailyTaskEntity> dailyTasks);
        Task<DailyTasksListEntity> GetDailyTasksListObj(DateOnly date);
        Task<DailyTasksListEntity> SaveChangesWithDailyTasksList(
            DailyTasksListEntity dailyTasksList);
        Task<List<DailyTaskEntity>> GetAllByDailyTasksListId(int id);
        Task<DailyTasksListEntity> DeleteDailyTasksList(
            DailyTasksListEntity dailyTasksList);
        Task<List<DailyTaskEntity>> GetDailyTasksById(int id);
    }
}
