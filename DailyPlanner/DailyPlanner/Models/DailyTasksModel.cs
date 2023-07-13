using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.Repository.Repos;
using DailyPlanner.StaticClasses;

namespace DailyPlanner.Models
{
    public class DailyTasksModel : IDailyTasksModel
    {
        public DateOnly Date { get; set; } = DateStatic.Date;
        public List<DailyTaskEntity> DailyTasks { get; set; } = new();
        public DailyTasksListEntity DailyTasksList { get; set; } = new();
        public List<string> ErrorsMessagesList { get; set; } = new();

        public int NumTasks { get => DailyTasks.Count; }
        public string DateString
        {
            get => Date.ToString();
            set
            {
                try
                {
                    string[] date = value.Split(".");
                    Date = new(Convert.ToInt32(date[2]),
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
        public bool CorrectInputData { get; set; } = true;

        public async Task<DailyTasksModel> GetFromDbDailyTasksModel(
            IDailyTasksRepository _dailyTasksRepository)
        {
            await GetFromDbDailyTasksList(_dailyTasksRepository);
            await GetFromDbDailyTasks(_dailyTasksRepository);

            return this;
        }
        public async Task<DailyTasksListEntity> GetFromDbDailyTasksList(
            IDailyTasksRepository _dailyTasksRepository)
        {
            DailyTasksList = 
                await _dailyTasksRepository.GetDailyTasksListObj(Date);

            return DailyTasksList;
        }
        public async Task<List<DailyTaskEntity>> GetFromDbDailyTasks(
            IDailyTasksRepository _dailyTasksRepository)
        {
            DailyTasks =
                await _dailyTasksRepository.GetDailyTasksById(DailyTasksList.Id);
            SetDailyTasksListsToEachDailyTaskObj();

            return DailyTasks;
        }

        public async Task<List<DailyTaskEntity>> SaveDailyTasksToDb(
            IDailyTasksRepository _dailyTasksRepository)
        {
            List<DailyTaskEntity> dbDailyTasks =
                await _dailyTasksRepository.GetAllByDailyTasksListId(
                    DailyTasksList.Id);

            List<DailyTaskEntity> dailyTasks = DailyTasks;

            for(int i = 0; i < dailyTasks.Count; i++)
            {
                if (dailyTasks[i].TaskDescription == null)
                {
                    dailyTasks.Remove(dailyTasks[i]);
                    i--;
                }
            }

            for (int i = 0; i < dailyTasks.Count; i++)
            {
                for (int j = 0; j < dbDailyTasks.Count; j++)
                {
                    if (dailyTasks[i].TaskDescription == 
                        dbDailyTasks[j].TaskDescription 
                        &&
                        dailyTasks[i].Importance == 
                        dbDailyTasks[j].Importance 
                        &&
                        dailyTasks[i].Status ==
                        dbDailyTasks[j].Status)
                    {
                        dailyTasks.Remove(dailyTasks[i]);
                        dbDailyTasks.Remove(dbDailyTasks[j]);

                        i--; j--;
                        break;
                    }
                }
            }

            while (dailyTasks.Count > 0 || dbDailyTasks!.Count > 0)
            {
                if(dailyTasks.Count > 0 && dbDailyTasks.Count > 0)
                {
                    await _dailyTasksRepository.UpdateAsync(
                        dbDailyTasks[0], dailyTasks[0]);

                    dailyTasks.Remove(dailyTasks[0]);
                    dbDailyTasks.Remove(dbDailyTasks[0]);
                }
                else if(dailyTasks.Count > 0)
                {
                    await _dailyTasksRepository.AddAsync(dailyTasks[0]);
                    dailyTasks.Remove(dailyTasks[0]);
                }
                else
                {
                    await _dailyTasksRepository.DeleteAsync(dbDailyTasks[0]);
                    dbDailyTasks.Remove(dbDailyTasks[0]);
                }
            }

            return DailyTasks;
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
    }
}
