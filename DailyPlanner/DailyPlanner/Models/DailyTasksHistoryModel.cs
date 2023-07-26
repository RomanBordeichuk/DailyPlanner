using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.Repository.Repos;
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

        public bool CorrectInputData { get; set; } = true;
        public List<string> ErrorMessagesList { get; set; } = new();
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

        public async Task<bool> SaveChangesWithDailyTasks()
        {
            if(DailyTasksRepository == null)
            {
                throw new Exception("DailyTasksRepository not found");
            }
            if(CurrentDailyTasksList == null)
            {
                throw new Exception("CurrentDailyTasksList not found");
            }

            List<DailyTaskEntity> dbDailyTasks =
                await DailyTasksRepository.GetDailyTasksByListId(CurrentDailyTasksList.Id);

            List<DailyTaskEntity> dailyTasks = CurrentDailyTasks;

            dailyTasks = DeleteFieldsWithEmptyTaskDescription(dailyTasks);

            (dbDailyTasks, dailyTasks) = DeleteEqualFieldsFromTwoDailyTasksLists(
                dbDailyTasks, dailyTasks);

            await SaveChangesWithDailyTasksToDb(dbDailyTasks, dailyTasks);

            return true;
        }

        private async Task<bool> SaveChangesWithDailyTasksToDb(
            List<DailyTaskEntity> dbDailyTasks,
            List<DailyTaskEntity> dailyTasks)
        {
            if (DailyTasksRepository == null)
            {
                throw new Exception("DailyTasksRepository not found");
            }

            while (dbDailyTasks.Count > 0)
            {
                if (dailyTasks.Count > 0 && dbDailyTasks.Count > 0)
                {
                    await DailyTasksRepository.UpdateAsync(
                        dbDailyTasks[0], dailyTasks[0]);

                    dbDailyTasks.Remove(dbDailyTasks[0]);
                    dailyTasks.Remove(dailyTasks[0]);
                }
                else
                {
                    await DailyTasksRepository.DeleteAsync(dbDailyTasks[0]);
                    dbDailyTasks.Remove(dbDailyTasks[0]);
                }
            }

            return true;
        }
        private (List<DailyTaskEntity>, List<DailyTaskEntity>)
            DeleteEqualFieldsFromTwoDailyTasksLists(
                List<DailyTaskEntity> dbDailyTasks,
                List<DailyTaskEntity> dailyTasks)
        {
            for (int i = 0; i < dbDailyTasks.Count; i++)
            {
                for (int j = 0; j < dailyTasks.Count; j++)
                {
                    if (dbDailyTasks[i].TaskDescription ==
                        dailyTasks[j].TaskDescription
                        &&
                        dbDailyTasks[i].Status ==
                        dailyTasks[j].Status
                        &&
                        dbDailyTasks[i].Importance ==
                        dailyTasks[j].Importance)
                    {
                        dbDailyTasks.Remove(dbDailyTasks[i]);
                        dailyTasks.Remove(dailyTasks[j]);

                        i--; j--;

                        break;
                    }
                }
            }

            return (dbDailyTasks, dailyTasks);
        }
        private List<DailyTaskEntity> DeleteFieldsWithEmptyTaskDescription(
            List<DailyTaskEntity> dailyTasks)
        {
            for (int i = 0; i < dailyTasks.Count; i++)
            {
                if (dailyTasks[i].TaskDescription == null)
                {
                    dailyTasks.Remove(dailyTasks[i]);
                    i--;
                }
            }

            return dailyTasks;
        }
    }
}
