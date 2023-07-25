using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using XAct;

namespace DailyPlanner.Models
{
    public class DailyTasksHistoryModel
    {
        public List<List<DateOnly>> DatesList { get; set; } = new();
        public DateOnly CurrentDate { get; set; } = ChosenDateStatic.ChosenDate;
        public DailyTasksListEntity? CurrentDailyTasksList { get; set; }
        public List<DailyTaskEntity> CurrentDailyTasks { get; set; } = new();

        public IDailyTasksRepository? DailyTasksRepository { get; set; }
        public string CurrentDateString
        {
            get => CurrentDate.Day + "." + CurrentDate.Month + "." + CurrentDate.Year;
            set
            {
                string[] numMas = value.Split(".");

                CurrentDate = new(
                    Convert.ToInt32(numMas[2]),
                    Convert.ToInt32(numMas[1]),
                    Convert.ToInt32(numMas[0]));

                ChosenDateStatic.ChosenDate = CurrentDate;
            }
        }

        public string GetMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    throw new Exception("Incorrect month");
            }
        }

        public async Task<List<List<DateOnly>>> GetFromDbDatesList()
        {
            if(DailyTasksRepository == null)
            {
                throw new Exception("DailyTasksRepository not found");
            }

            List<DateOnly> dailyTasksDates = 
                await DailyTasksRepository.GetDailyTasksDays();

            DatesList = new();

            foreach(DateOnly date in dailyTasksDates)
            {
                bool hasMonthDate = false;

                for(int i = 0; i < DatesList.Count; i++)
                {
                    if (DatesList[i] != null &&
                        DatesList[i][0].Month == date.Month &&
                        DatesList[i][0].Year == date.Year)
                    {
                        hasMonthDate = true;

                        DatesList[i].Add(date);

                        break;
                    }
                }

                if (!hasMonthDate)
                {
                    DatesList.Add(new() { date });
                }
            }

            DatesList.Reverse();

            return DatesList;
        }

        public async Task<DailyTasksListEntity> GetFromDbCurrentDailyTasksList()
        {
            if (DailyTasksRepository == null)
            {
                throw new Exception("DailyTasksRepository not found");
            }

            CurrentDailyTasksList =
                await DailyTasksRepository.GetDailyTasksListObjByDate(CurrentDate);

            return CurrentDailyTasksList;
        }

        public async Task<List<DailyTaskEntity>> GetFromDbCurrentDailyTasks()
        {
            if(DailyTasksRepository == null)
            {
                throw new Exception("DailyTasksRepository not found");
            }

            if(CurrentDailyTasksList == null)
            {
                throw new Exception("CurrentDailyTasksList not found");
            }

            CurrentDailyTasks =
                await DailyTasksRepository.GetDailyTasksByListId(CurrentDailyTasksList.Id);

            return CurrentDailyTasks;
        }
    }
}
