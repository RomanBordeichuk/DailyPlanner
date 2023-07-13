using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository.Repos
{
    public class DailyTasksRepository : IDailyTasksRepository
    {
        private readonly AppDbContext _context;

        public DailyTasksRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DailyTaskEntity> AddAsync(DailyTaskEntity dailyTask)
        {
            DailyTaskEntity? dbDailyTask =
                await _context.DailyTasks.FirstOrDefaultAsync(
                    d => d.Id == dailyTask.Id);

            if (dbDailyTask != null)
            {
                dbDailyTask = dailyTask;
            }
            else
            {
                await _context.DailyTasks.AddAsync(dailyTask);
            }

            await _context.SaveChangesAsync();

            return dailyTask;
        }

        public async Task<DailyTaskEntity> GetByIdAsync(int id)
        {
            DailyTaskEntity? dailyTaskEntity =
                await _context.DailyTasks.FirstOrDefaultAsync(d => d.Id == id);

            if (dailyTaskEntity != null)
            {
                return dailyTaskEntity;
            }

            throw new Exception("Daily Task hand't found");
        }

        public async Task<List<DailyTaskEntity>> GetAllAsync()
        {
            List<DailyTaskEntity> dailyTasks = 
                await _context.DailyTasks.ToListAsync();

            return dailyTasks;
        }

        public async Task<List<DailyTaskEntity>> GetAllByDailyTasksListId(
            int id)
        {
            List<DailyTaskEntity> dailyTasks =
                await _context.DailyTasks.Where(
                    d => d.DailyTasksListId == id).ToListAsync();

            return dailyTasks;
        }

        public async Task<DailyTaskEntity> DeleteByIdAsync(int id)
        {
            DailyTaskEntity? dailyTask =
                await _context.DailyTasks.FirstOrDefaultAsync(d => d.Id == id);

            if (dailyTask != null)
            {
                _context.DailyTasks.Remove(dailyTask);
                await _context.SaveChangesAsync();
                return dailyTask;
            }

            throw new Exception("Daily Task had't found");
        }

        public async Task<DailyTaskEntity> UpdateByIdAsync(
            int id, DailyTaskEntity dailyTask)
        {
            DailyTaskEntity? dbDailyTask =
                await _context.DailyTasks.FirstOrDefaultAsync(d => d.Id == id);

            if (dbDailyTask != null)
            {
                dbDailyTask = dailyTask;
                await _context.SaveChangesAsync();
                return dailyTask;
            }

            throw new Exception("Daily Task had't found");
        }

        public async Task<DailyTaskEntity> UpdateAsync(
            DailyTaskEntity oldDailyTask, DailyTaskEntity newDailyTask)
        {
            DailyTaskEntity dbDailyTask =
                await _context.DailyTasks.FirstAsync(
                    d => d.Id == oldDailyTask.Id);

            dbDailyTask.TaskDescription = newDailyTask.TaskDescription;
            dbDailyTask.Importance = newDailyTask.Importance;
            dbDailyTask.Status = newDailyTask.Status;
            dbDailyTask.DailyTasksList = newDailyTask.DailyTasksList;

            await _context.SaveChangesAsync();

            return newDailyTask;
        }

        public async Task<DailyTaskEntity> DeleteAsync(
            DailyTaskEntity dailyTask)
        {
            if (await _context.DailyTasks.ContainsAsync(dailyTask))
            {
                _context.DailyTasks.Remove(dailyTask);
                await _context.SaveChangesAsync();

                return dailyTask;
            }

            return new();
        }

        public async Task<List<DailyTaskEntity>> AddListAsync(
            List<DailyTaskEntity> dailyTasks)
        {
            await _context.AddRangeAsync(dailyTasks);
            await _context.SaveChangesAsync();

            return dailyTasks;
        }

        public async Task<DailyTasksListEntity> GetDailyTasksListObj(
            DateOnly date)
        {
            if (CurrentUserStatic.User != null)
            {
                DailyTasksListEntity? dailyTasksList =
                    await _context.DailyTasksLists.FirstOrDefaultAsync(l =>
                    l.UserEntityId == CurrentUserStatic.User.Id &&
                    l.Date.Year == date.Year &&
                    l.Date.Month == date.Month &&
                    l.Date.Day == date.Day);

                if (dailyTasksList != null)
                {
                    return dailyTasksList;
                }

                DailyTasksListEntity newDailyTasksList = new()
                {
                    Date = new(date.Year, date.Month, date.Day),
                    UserEntityId = CurrentUserStatic.User.Id
                };

                return newDailyTasksList;
            }

            throw new Exception("User not found");
        } 

        public async Task<DailyTasksListEntity> SaveChangesWithDailyTasksList(
            DailyTasksListEntity dailyTasksList)
        {
            DailyTasksListEntity? dbDailyTasksList = 
                await _context.DailyTasksLists.FirstOrDefaultAsync(
                    l => l.Id == dailyTasksList.Id);

            if(dbDailyTasksList != null)
            {
                dbDailyTasksList = dailyTasksList;
            }
            else
            {
                await _context.DailyTasksLists.AddAsync(dailyTasksList);
            }

            await _context.SaveChangesAsync();

            return dailyTasksList;
        }

        public async Task<DailyTasksListEntity> DeleteDailyTasksList(
            DailyTasksListEntity dailyTasksList)
        {
            _context.DailyTasksLists.Remove(dailyTasksList);
            await _context.SaveChangesAsync();

            return dailyTasksList;
        }

        public async Task<List<DailyTaskEntity>> GetDailyTasksById(int id)
        {
            List<DailyTaskEntity>? dailyTasks =
                await _context.DailyTasks.Where(
                    d => d.DailyTasksListId == id).ToListAsync();

            if (dailyTasks != null && dailyTasks.Count > 0)
            {
                return dailyTasks;
            }
            else
            {
                return new()
                {
                    new(),
                    new(),
                    new()
                };
            }
        }
    }
}
