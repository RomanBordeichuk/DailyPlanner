using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IDailyTasksListEntity : IEntity
    {
        DateTime Date { get; set; }
        List<DailyTaskEntity>? DailyTasks { get; set; }
        int UserEntityId { get; set; }
        UserEntity? UserEntity { get; set; }
    }
}
