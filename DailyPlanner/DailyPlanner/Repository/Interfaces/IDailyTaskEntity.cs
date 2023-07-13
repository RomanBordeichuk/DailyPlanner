using DailyPlanner.Enums;
using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IDailyTaskEntity : IEntity
    {
        string? TaskDescription { get; set; }
        Importance Importance { get; set; }
        DailyTaskStatus Status { get; set; }
        public int DailyTasksListId { get; set; }
        DailyTasksListEntity? DailyTasksList { get; set; }
    }
}
