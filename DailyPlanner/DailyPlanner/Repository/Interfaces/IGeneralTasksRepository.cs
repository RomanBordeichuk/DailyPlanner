using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IGeneralTasksRepository : IBaseRepository
    {
        Task<List<GeneralTaskEntity>> GetGeneralTasksInProcess();
        Task<GeneralTaskEntity> AddAsync(GeneralTaskEntity generalTask);
        Task<GeneralTaskEntity> UpdateAsync(
            GeneralTaskEntity oldGeneralTask, GeneralTaskEntity newGeneralTask);
        Task<GeneralTaskEntity> DeleteAsync(GeneralTaskEntity generalTask);
    }
}
