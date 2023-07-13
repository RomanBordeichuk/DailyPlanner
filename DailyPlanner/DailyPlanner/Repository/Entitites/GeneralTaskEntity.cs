using DailyPlanner.Enums;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Repository.Entitites
{
    public class GeneralTaskEntity : IGeneralTaskEntity
    {
        public int Id { get; set; }
        public string? TaskDescription { get; set; }
        public GeneralTaskStatus Status { get; set; }
        public DateTime DeadLine { get; set; }
        public DateTime ExecutionDate { get; set; }
        public int UserEntityId { get; set; }
        public UserEntity? UserEntity { get; set; }
    }
}
