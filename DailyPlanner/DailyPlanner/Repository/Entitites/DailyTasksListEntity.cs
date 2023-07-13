using DailyPlanner.Repository.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyPlanner.Repository.Entitites
{
    public class DailyTasksListEntity : IDailyTasksListEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }
        public List<DailyTaskEntity>? DailyTasks { get; set; }
        public int UserEntityId { get; set; }
        public UserEntity? UserEntity { get; set; }
    }
}
