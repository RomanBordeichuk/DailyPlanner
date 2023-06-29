using DailyPlanner.Enums;

namespace DailyPlanner.Interfaces
{
    public interface IDailyTaskModel : IModel
    {
        string TaskDescription { get; set; }
        Importance Importance { get; set; }
        Status Status { get; set; }
    }
}

