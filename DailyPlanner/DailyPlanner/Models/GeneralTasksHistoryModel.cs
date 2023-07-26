using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using XAct;

namespace DailyPlanner.Models
{
    public class GeneralTasksHistoryModel
    {
        public List<(int, int)> MonthDatesList { get; set; } = new();
        public (int, int) CurrentMonthNum { get; set; } = ChosenDateStatic.ChosenMonth;
        public List<GeneralTaskEntity> CurrentMonthTasksList { get; set; } = new();
        public bool CorrectInputData { get; set; } = true;
        public List<string> ErrorMessagesList { get; set; } = new();
        public IGeneralTasksRepository? GeneralTasksRepository { get; set; }
        public string CurrentMonthString
        {
            get => CurrentMonthNum.Item1 + "." + CurrentMonthNum.Item2; 
            set
            {
                string[] numsMas = value.Split(".");

                CurrentMonthNum = 
                    (Convert.ToInt32(numsMas[0]), Convert.ToInt32(numsMas[1]));

                ChosenDateStatic.ChosenMonth = CurrentMonthNum;
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

        public async Task<List<(int, int)>> GetFromDbMonthDatesList()
        {
            if(GeneralTasksRepository == null)
            {
                throw new Exception("GeneralTasksRepository not found");
            }

            List<DateOnly> datesList =
                await GeneralTasksRepository.GetDatesList();

            MonthDatesList = CreateMonthDatesList(datesList);

            MonthDatesList.Reverse();

            return MonthDatesList;
        }

        public async Task<List<GeneralTaskEntity>> GetFromDbCurrentMonthGeneralTasks()
        {
            if(GeneralTasksRepository == null)
            {
                throw new Exception("GeneralTasksRepository not found");
            }

            CurrentMonthTasksList = 
                await GeneralTasksRepository.GetGeneralTasksByExecutionDateMonth(
                    CurrentMonthNum.Item1, CurrentMonthNum.Item2);

            return CurrentMonthTasksList;
        }

        public async Task<bool> SaveChangesWithGeneralTasks()
        {
            if(GeneralTasksRepository == null)
            {
                throw new Exception("GeneralTasksRepository not found");
            }

            List<GeneralTaskEntity> dbGeneralTasks =
                await GeneralTasksRepository.GetGeneralTasksByExecutionDateMonth(
                    CurrentMonthNum.Item1, CurrentMonthNum.Item2);

            List<GeneralTaskEntity> generalTasks = CurrentMonthTasksList;

            generalTasks = DeleteFieldsWithEmptyTaskDescription(generalTasks);

            (dbGeneralTasks, generalTasks) = DeleteEqualFieldsFromTwoGeneralTasksLists(
                dbGeneralTasks, generalTasks);

            await SaveChangesWithGeneralTasksToDb(dbGeneralTasks, generalTasks);

            return true;
        }

        private async Task<bool> SaveChangesWithGeneralTasksToDb(
            List<GeneralTaskEntity> dbGeneralTasks,
            List<GeneralTaskEntity> generalTasks)
        {
            if (GeneralTasksRepository == null)
            {
                throw new Exception("GeneralTasksRepository not found");
            }

            while (dbGeneralTasks.Count > 0)
            {
                if (generalTasks.Count > 0 && dbGeneralTasks.Count > 0)
                {
                    await GeneralTasksRepository.UpdateAsync(
                        dbGeneralTasks[0], generalTasks[0]);

                    dbGeneralTasks.Remove(dbGeneralTasks[0]);
                    generalTasks.Remove(generalTasks[0]);
                }
                else
                {
                    await GeneralTasksRepository.DeleteAsync(dbGeneralTasks[0]);
                    dbGeneralTasks.Remove(dbGeneralTasks[0]);
                }
            }

            return true;
        }

        public void CheckGeneralTasksInputData()
        {
            foreach(GeneralTaskEntity generalTask in CurrentMonthTasksList)
            {
                if(!generalTask.CorrectDeadLine)
                {
                    CorrectInputData = false;
                    ErrorMessagesList.Add("Some deadline dates are incorrect");
                    break;
                }
            }

            foreach (GeneralTaskEntity generalTask in CurrentMonthTasksList)
            {
                if (!generalTask.CorrectExecutionDate)
                {
                    CorrectInputData = false;
                    ErrorMessagesList.Add("Some execution dates are incorrect");
                    break;
                }
            }
        }
        private List<(int, int)> CreateMonthDatesList(
            List<DateOnly> datesList)
        {
            List<(int, int)> uniqueMonthDatesList = new();

            foreach (DateOnly date in datesList)
            {
                if(date != new DateOnly() && 
                    !uniqueMonthDatesList.Contains((date.Month, date.Year)))
                {
                    uniqueMonthDatesList.Add((date.Month, date.Year));
                }
            }

            return uniqueMonthDatesList;
        }
        private (List<GeneralTaskEntity>, List<GeneralTaskEntity>)
            DeleteEqualFieldsFromTwoGeneralTasksLists(
                List<GeneralTaskEntity> dbGeneralTasks,
                List<GeneralTaskEntity> generalTasks)
        {
            for (int i = 0; i < dbGeneralTasks.Count; i++)
            {
                for (int j = 0; j < generalTasks.Count; j++)
                {
                    if (dbGeneralTasks[i].TaskDescription ==
                        generalTasks[j].TaskDescription
                        &&
                        dbGeneralTasks[i].Status ==
                        generalTasks[j].Status
                        &&
                        dbGeneralTasks[i].DeadLine ==
                        generalTasks[j].DeadLine)
                    {
                        dbGeneralTasks.Remove(dbGeneralTasks[i]);
                        generalTasks.Remove(generalTasks[j]);

                        i--; j--;

                        break;
                    }
                }
            }

            return (dbGeneralTasks, generalTasks);
        }
        private List<GeneralTaskEntity> DeleteFieldsWithEmptyTaskDescription(
            List<GeneralTaskEntity> generalTasks)
        {
            for (int i = 0; i < generalTasks.Count; i++)
            {
                if (generalTasks[i].TaskDescription == null)
                {
                    generalTasks.Remove(generalTasks[i]);
                    i--;
                }
            }

            return generalTasks;
        }
    }
}
