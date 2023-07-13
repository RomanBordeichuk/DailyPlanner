using DailyPlanner.Enums;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Repository.Entitites
{
    public class DailyTaskEntity : IDailyTaskEntity
    {
        public int Id { get; set; }
        public string? TaskDescription { get; set; }
        public Importance Importance { get; set; }
        public DailyTaskStatus Status { get; set; }
        public int DailyTasksListId { get; set; }
        public DailyTasksListEntity? DailyTasksList { get; set; }
    }
}
