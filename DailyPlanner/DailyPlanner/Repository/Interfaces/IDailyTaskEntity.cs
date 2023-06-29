using DailyPlanner.Enums;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IDailyTaskEntity : IEntity
    {
        string TaskDescription { get; set; }
        Importance Importance { get; set; }
        Status Status { get; set; }
    }
}
