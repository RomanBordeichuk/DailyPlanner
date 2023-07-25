using DailyPlanner.Enums;
using DailyPlanner.Repository.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DailyPlanner.Repository.Entitites
{
    public class GeneralTaskEntity : IGeneralTaskEntity
    {
        public int Id { get; set; }
        public string? TaskDescription { get; set; }
        public GeneralTaskStatus Status { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DeadLine { get; set; }

        [Column(TypeName = "Date")]
        public DateTime ExecutionDate { get; set; }
        public int UserEntityId { get; set; }
        public UserEntity? UserEntity { get; set; }

        [NotMapped]
        public bool CorrectDeadLine { get; set; } = true;
        [NotMapped]
        public bool CorrectExecutionDate { get; set; } = true;

        [NotMapped]
        public string DeadLineString
        {
            get
            {
                if(DeadLine == new DateTime())
                {
                    return "Without deadline";
                }

                return new DateOnly(DeadLine.Year, DeadLine.Month, DeadLine.Day).ToString();
            }
            set
            {
                if(value == "Without deadline")
                {
                    DeadLine = new();
                    return;
                }

                try
                {
                    string[] date = value.Split(".");
                    DeadLine = new(
                        Convert.ToInt32(date[2]),
                        Convert.ToInt32(date[1]),
                        Convert.ToInt32(date[0]));
                }
                catch
                {
                    CorrectDeadLine = false;
                }
            }
        }

        [NotMapped]
        public string ExecutionDateString
        {
            get
            {
                if (ExecutionDate == new DateTime())
                {
                    return "Not executed yet";
                }

                return new DateOnly(
                    ExecutionDate.Year, 
                    ExecutionDate.Month, 
                    ExecutionDate.Day).ToString();
            }
            set
            {
                if (value == "Not executed yet")
                {
                    ExecutionDate = new();
                    return;
                }

                try
                {
                    string[] date = value.Split(".");
                    ExecutionDate = new(
                        Convert.ToInt32(date[2]),
                        Convert.ToInt32(date[1]),
                        Convert.ToInt32(date[0]));
                }
                catch
                {
                    CorrectExecutionDate = false;
                }
            }
        }
    }
}
