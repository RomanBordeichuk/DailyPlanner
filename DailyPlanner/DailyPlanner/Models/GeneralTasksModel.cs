using DailyPlanner.Enums;
using DailyPlanner.Interfaces;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;

namespace DailyPlanner.Models
{
    public class GeneralTasksModel : IGeneralTasksModel
    {
        public DateOnly Date { get; set; } = DateStatic.Date;
        public List<GeneralTaskEntity> GeneralTasks { get; set; } = new();
        public bool CorrectInputData { get; set; } = true;
        public List<string> ErrorMessagesList { get; set; } = new();
        public IGeneralTasksRepository? GeneralTasksRepository { get; set; }

        public int NumTasks { get => GeneralTasks.Count; }
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

                    DateStatic.Date = Date;
                }
                catch
                {
                    CorrectInputData = false;
                    ErrorMessagesList.Add("Incorrect date");
                }
            }
        }

        public async Task<GeneralTasksModel> GetFromDbGeneralTasks()
        {
            if(GeneralTasksRepository != null)
            {
                GeneralTasks =
                    await GeneralTasksRepository.GetGeneralTasksInProcess();
                SetUserEntityIdToEachGeneralTask();

                return this;
            }

            throw new Exception("GeneralTasksRepository not found");
        }
        public async Task<bool> SaveGeneralTasksToDb()
        {
            if(GeneralTasksRepository != null)
            {
                SetUserEntityIdToEachGeneralTask();

                List<GeneralTaskEntity> dbGeneralTasks =
                    await GeneralTasksRepository.GetGeneralTasksInProcess();

                List<GeneralTaskEntity> generalTasks = GeneralTasks;

                generalTasks = DeleteFieldsWithEmptyTaskDescription(generalTasks);
                dbGeneralTasks = DeleteFieldsWithEmptyTaskDescription(dbGeneralTasks);

                (dbGeneralTasks, generalTasks) = DeleteEqualFieldsFromTwoGeneralTasksLists(
                    dbGeneralTasks, generalTasks);

                generalTasks = SetExecutionDatesIfNeeded(generalTasks);

                await SaveChangesWithGeneralTasksToDb(dbGeneralTasks, generalTasks);

                return true;
            }

            throw new Exception("GeneralTasksRepository not found");
        }

        public void SetUserEntityIdToEachGeneralTask()
        {
            if(CurrentUserStatic.User != null)
            {
                foreach (GeneralTaskEntity generalTask in GeneralTasks)
                {
                    generalTask.UserEntityId = CurrentUserStatic.User.Id;
                }

                return;
            }

            throw new Exception("User not found");
        }
        public bool CorrectGeneralTasksDates()
        {
            foreach(GeneralTaskEntity generalTask in GeneralTasks)
            {
                if (!generalTask.CorrectDeadLine) return false; 
            }

            return true;
        }

        private List<GeneralTaskEntity> DeleteFieldsWithEmptyTaskDescription(
            List<GeneralTaskEntity> generalTasks)
        {
            for(int i = 0; i < generalTasks.Count; i++)
            {
                if (generalTasks[i].TaskDescription == null)
                {
                    generalTasks.Remove(generalTasks[i]);
                    i--;
                }
            }

            return generalTasks;
        }
        private (List<GeneralTaskEntity>, List<GeneralTaskEntity>) 
            DeleteEqualFieldsFromTwoGeneralTasksLists(
                List<GeneralTaskEntity> dbGeneralTasks, 
                List<GeneralTaskEntity> generalTasks)
        {
            for(int i = 0; i < dbGeneralTasks.Count; i++)
            {
                for(int j = 0; j < generalTasks.Count; j++)
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
        private List<GeneralTaskEntity> SetExecutionDatesIfNeeded(
            List<GeneralTaskEntity> generalTasks)
        {
            foreach(GeneralTaskEntity generalTask in generalTasks)
            {
                if(generalTask.Status == GeneralTaskStatus.Completed)
                {
                    generalTask.ExecutionDate = new(Date.Year, Date.Month, Date.Day);
                }
            }

            return generalTasks;
        }
        private async Task<bool> SaveChangesWithGeneralTasksToDb(
            List<GeneralTaskEntity> dbGeneralTasks, 
            List<GeneralTaskEntity> generalTasks)
        {
            if(GeneralTasksRepository != null)
            {
                while (dbGeneralTasks.Count > 0 || generalTasks.Count > 0)
                {
                    if (generalTasks.Count > 0 && dbGeneralTasks.Count > 0)
                    {
                        await GeneralTasksRepository.UpdateAsync(
                            dbGeneralTasks[0], generalTasks[0]);

                        dbGeneralTasks.Remove(dbGeneralTasks[0]);
                        generalTasks.Remove(generalTasks[0]);
                    }
                    else if (generalTasks.Count > 0)
                    {
                        await GeneralTasksRepository.AddAsync(generalTasks[0]);
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

            throw new Exception("GeneralTasksRepository not found");
        }
    }
}
