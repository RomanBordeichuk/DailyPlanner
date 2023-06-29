using DailyPlanner.Enums;
using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Entitites;
using System.ComponentModel.DataAnnotations;

namespace DailyPlanner.Models
{
    public class DailyTaskModel : IDailyTaskModel
    {
        [Required]
        [MinLength(5, ErrorMessage = 
            "Task description legth must be at least 5 letters")]
        public string TaskDescription { get; set; } = null!;
        public Importance Importance { get; set; }
        public Status Status { get; set; }

        public DailyTaskModel(string taskDescription,
            Importance importance, Status status)
        {
            TaskDescription = taskDescription;
            Importance = importance;
            Status = status;
        }
        public DailyTaskModel(DailyTaskEntity dailyTask)
        {
            TaskDescription = dailyTask.TaskDescription;
            Importance = dailyTask.Importance;
            Status = dailyTask.Status;
        }
    }
}
