using DailyPlanner.Enums;
using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Repository.Entitites
{
    public class DailyTaskEntity : IDailyTaskEntity
    {
        public int Id { get; set; }
        public string TaskDescription { get; set; } = null!;
        public Importance Importance { get; set; }
        public Status Status { get; set; }

        public DailyTaskEntity(string taskDescription, 
            Importance importance, Status status)
        {
            TaskDescription = taskDescription;
            Importance = importance;
            Status = status;
        }
        public DailyTaskEntity(DailyTaskModel dailyTask)
        {
            TaskDescription = dailyTask.TaskDescription;
            Importance = dailyTask.Importance;
            Status = dailyTask.Status;
        }
    }
}
