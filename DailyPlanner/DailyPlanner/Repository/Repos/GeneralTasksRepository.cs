using DailyPlanner.Enums;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository.Repos
{
    public class GeneralTasksRepository : IGeneralTasksRepository
    {
        private readonly AppDbContext _context;

        public GeneralTasksRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GeneralTaskEntity>> GetGeneralTasksInProcess()
        {
            if (CurrentUserStatic.User != null)
            {
                List<GeneralTaskEntity>? generalTasks =
                    await _context.GeneralTasks.Where(g =>
                        g.UserEntityId == CurrentUserStatic.User.Id &&
                        g.Status == GeneralTaskStatus.InProcess).ToListAsync();

                if (generalTasks != null && generalTasks.Count > 0)
                {
                    return generalTasks;
                }

                return new()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new()
                };
            }

            throw new Exception("User not found");
        }
        public async Task<GeneralTaskEntity> AddAsync(GeneralTaskEntity generalTask)
        {
            await _context.GeneralTasks.AddAsync(generalTask);
            await _context.SaveChangesAsync();

            return generalTask;
        }
        public async Task<GeneralTaskEntity> UpdateAsync(
            GeneralTaskEntity oldGeneralTask, GeneralTaskEntity newGeneralTask)
        {
            GeneralTaskEntity? dbGeneralTaskEntity =
                await _context.GeneralTasks.FirstOrDefaultAsync(g =>
                    g.UserEntityId == oldGeneralTask.UserEntityId &&
                    g.TaskDescription == oldGeneralTask.TaskDescription &&
                    g.Status == oldGeneralTask.Status &&
                    g.DeadLine == oldGeneralTask.DeadLine);

            if (dbGeneralTaskEntity != null)
            {
                dbGeneralTaskEntity.TaskDescription = newGeneralTask.TaskDescription;
                dbGeneralTaskEntity.Status = newGeneralTask.Status;
                dbGeneralTaskEntity.DeadLine = newGeneralTask.DeadLine;
                dbGeneralTaskEntity.ExecutionDate = newGeneralTask.ExecutionDate;

                await _context.SaveChangesAsync();

                return dbGeneralTaskEntity;
            }

            throw new Exception("GeneralTaskEntity not found");
        }
        public async Task<GeneralTaskEntity> DeleteAsync(GeneralTaskEntity generalTask)
        {
            GeneralTaskEntity? dbGeneralTaskEntity =
                await _context.GeneralTasks.FirstOrDefaultAsync(g =>
                    g.UserEntityId == generalTask.UserEntityId &&
                    g.TaskDescription == generalTask.TaskDescription &&
                    g.Status == generalTask.Status &&
                    g.DeadLine == generalTask.DeadLine);

            if (dbGeneralTaskEntity != null)
            {
                _context.GeneralTasks.Remove(dbGeneralTaskEntity);
                await _context.SaveChangesAsync();

                return dbGeneralTaskEntity;
            }

            throw new Exception("GeneralTaskEntity not found");
        }

        public async Task<List<DateOnly>> GetDatesList()
        {
            if (CurrentUserStatic.User == null)
            {
                throw new Exception("User not found");
            }

            List<DateOnly>? monthDatesList =
                await _context.GeneralTasks.Where(g =>
                    g.UserEntityId == CurrentUserStatic.User.Id).Select(g =>
                        new DateOnly(
                            g.ExecutionDate.Year,
                            g.ExecutionDate.Month,
                            g.ExecutionDate.Day)).ToListAsync();

            if(monthDatesList != null)
            {
                return monthDatesList;
            }

            return new();
        }

        public async Task<List<GeneralTaskEntity>> GetGeneralTasksByExecutionDateMonth(
            int month, int year)
        {
            if(CurrentUserStatic.User == null)
            {
                throw new Exception("User not found");
            }

            List<GeneralTaskEntity>? generalTasksList =
                await _context.GeneralTasks.Where(g => 
                    g.UserEntityId == CurrentUserStatic.User.Id &&
                    g.ExecutionDate.Month == month &&
                    g.ExecutionDate.Year == year).ToListAsync();

            if(generalTasksList != null)
            {
                return generalTasksList;
            }

            return new()
            {
                new(),
                new(),
                new(),
                new(),
                new()
            };
        }

        public async Task<GeneralTaskEntity> GetClosesGeneralTaskByUserId(int userId)
        {
            List<GeneralTaskEntity>? orderedGeneralTasksList =
                await _context.GeneralTasks.Where(g => 
                    g.UserEntityId == userId &&
                    g.Status == GeneralTaskStatus.InProcess).OrderBy(g => 
                        g.DeadLine).ToListAsync();

            if(orderedGeneralTasksList == null)
            {
                return new();
            }

            foreach(GeneralTaskEntity generalTask in orderedGeneralTasksList)
            {
                if(!(generalTask.DeadLine == new DateTime()))
                {
                    return generalTask;
                }
            }

            return orderedGeneralTasksList[0];
        }
    }
}
