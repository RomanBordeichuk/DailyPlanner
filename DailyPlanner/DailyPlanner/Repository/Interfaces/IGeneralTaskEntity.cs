using DailyPlanner.Enums;
using DailyPlanner.Repository.Entitites;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IGeneralTaskEntity : IEntity
    {
        string? TaskDescription { get; set; }
        GeneralTaskStatus Status { get; set; }

        [Column(TypeName = "Date")]
        DateTime DeadLine { get; set; }

        [Column(TypeName = "Date")]
        DateTime ExecutionDate { get; set; }
        int UserEntityId { get; set; }
        UserEntity? UserEntity { get; set; }

        bool CorrectDeadLine { get; set; }
        bool CorrectExecutionDate { get; set; }

        string DeadLineString { get; set; }
        string ExecutionDateString { get; set; }
    }
}
