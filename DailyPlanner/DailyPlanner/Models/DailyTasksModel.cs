using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;

namespace DailyPlanner.Models
{
    public class DailyTasksModel
    {
        public DateOnly Date { get; set; } = DateStatic.Date;
        public List<DailyTaskEntity> DailyTasks { get; set; } = new();
        public DailyTasksListEntity DailyTasksList { get; set; } = new();
        public bool CorrectInputData { get; set; } = true;
        public List<string> ErrorsMessagesList { get; set; } = new();
        public IDailyTasksRepository? DailyTasksRepository { get; set; }

        public int NumTasks { get => DailyTasks.Count; }
        public string DateString
        {
            get => Date.ToString();
            set
            {
                try
                {
                    string[] date = value.Split(".");
                    Date = new(
                        Convert.ToInt32(date[2]),
                        Convert.ToInt32(date[1]),
                        Convert.ToInt32(date[0]));
                }
                catch 
                {
                    CorrectInputData = false;
                    ErrorsMessagesList.Add("Incorrect Date");
                }
            }
        }

        public async Task<DailyTasksModel> GetFromDbDailyTasksModel()
        {
            if(DailyTasksRepository != null)
            {
                await GetFromDbDailyTasksList();
                await GetFromDbDailyTasks();

                return this;
            }

            throw new Exception("DailyTasksRepository not found");
        }
        public async Task<DailyTasksListEntity> GetFromDbDailyTasksList()
        {
            if(DailyTasksRepository != null)
            {
                DailyTasksList =
                    await DailyTasksRepository.GetDailyTasksListObjByDate(Date);

                return DailyTasksList;
            }

            throw new Exception("DailyTasksRepository not found");
        }
        public async Task<List<DailyTaskEntity>> GetFromDbDailyTasks()
        {
            if(DailyTasksRepository != null)
            {
                DailyTasks =
                    await DailyTasksRepository.GetDailyTasksById(DailyTasksList.Id);
                SetDailyTasksListsToEachDailyTaskObj();

                return DailyTasks;
            }

            throw new Exception("DailyTasksRepository not found");
        }
        public async Task<List<DailyTaskEntity>> SaveDailyTasksToDb()
        {
            if(DailyTasksRepository != null)
            {
                SetDailyTasksListsToEachDailyTaskObj();

                List<DailyTaskEntity> dbDailyTasks =
                    await DailyTasksRepository.GetDailyTasksById(
                        DailyTasksList.Id);

                List<DailyTaskEntity> dailyTasks = DailyTasks;

                dbDailyTasks = DeleteEmptyFieldsFromDailyTasks(dbDailyTasks);
                dailyTasks = DeleteEmptyFieldsFromDailyTasks(dailyTasks);

                await DeleteEmptyDailyTasksListIfNeeded(dailyTasks);

                (dbDailyTasks, dailyTasks) = DeleteEqualFieldsFromTwoDailyTaskLists(
                    dbDailyTasks, dailyTasks);

                await SaveChangesWithDailyTasksToDb(dbDailyTasks, dailyTasks);

                return DailyTasks;
            }

            throw new Exception("DailyTasksRepository not found");
        }

        public void ChangeGlobalDate()
        {
            DateStatic.Date = Date;
        }
        public void BackUpToGlobalDate()
        {
            Date = DateStatic.Date;
        }

        public void SetDailyTasksListsToEachDailyTaskObj()
        {
            foreach (DailyTaskEntity dailyTask in DailyTasks)
            {
                dailyTask.DailyTasksList = DailyTasksList;
            }
        }

        public bool DateChanged()
        {
            if(Date != DateStatic.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<DailyTaskEntity> DeleteEmptyFieldsFromDailyTasks(
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
        private (List<DailyTaskEntity>, List<DailyTaskEntity>) 
            DeleteEqualFieldsFromTwoDailyTaskLists(
            List<DailyTaskEntity> firstList, List<DailyTaskEntity> secondList)
        {
            for (int i = 0; i < firstList.Count; i++)
            {
                for (int j = 0; j < secondList.Count; j++)
                {
                    if (firstList[i].TaskDescription ==
                        secondList[j].TaskDescription
                        &&
                        firstList[i].Importance ==
                        secondList[j].Importance
                        &&
                        firstList[i].Status ==
                        secondList[j].Status)
                    {
                        firstList.Remove(firstList[i]);
                        secondList.Remove(secondList[j]);

                        i--; j--;
                        break;
                    }
                }
            }

            return (firstList, secondList);
        }

        private async Task<bool> SaveChangesWithDailyTasksToDb(
            List<DailyTaskEntity> dbDailyTasks, List<DailyTaskEntity> dailyTasks)
        {
            if(DailyTasksRepository != null)
            {
                while (dailyTasks.Count > 0 || dbDailyTasks!.Count > 0)
                {
                    if (dailyTasks.Count > 0 && dbDailyTasks.Count > 0)
                    {
                        await DailyTasksRepository.UpdateAsync(
                            dbDailyTasks[0], dailyTasks[0]);

                        dailyTasks.Remove(dailyTasks[0]);
                        dbDailyTasks.Remove(dbDailyTasks[0]);
                    }
                    else if (dailyTasks.Count > 0)
                    {
                        await DailyTasksRepository.AddAsync(dailyTasks[0]);
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

            throw new Exception("DailyTasksRepository not found");
        }
        private async Task<bool> DeleteEmptyDailyTasksListIfNeeded(
            List<DailyTaskEntity> dailyTasks)
        {
            if(dailyTasks == null)
            {
                if(DailyTasksRepository != null)
                {
                    await DailyTasksRepository.DeleteDailyTasksList(DailyTasksList);

                    return true;
                }

                throw new Exception("DailyTasksRepository not found");
            }

            return false;
        }
    }
}
