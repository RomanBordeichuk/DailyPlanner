using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<DailyTaskEntity> UpdateAsync(
            DailyTaskEntity oldDailyTask, DailyTaskEntity newDailyTask)
        {
            DailyTaskEntity dbDailyTask =
                await _context.DailyTasks.FirstAsync(
                    d => d.Id == oldDailyTask.Id);

            dbDailyTask.TaskDescription = newDailyTask.TaskDescription;
            dbDailyTask.Importance = newDailyTask.Importance;
            dbDailyTask.Status = newDailyTask.Status;

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

        public async Task<DailyTasksListEntity> GetDailyTasksListObjByDate(
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

        public async Task<List<DateOnly>> GetDailyTasksDays()
        {
            if(CurrentUserStatic.User == null)
            {
                throw new Exception("User not found");
            }

            List<DateOnly>? daysList = await _context.DailyTasksLists.Where(l =>
                l.UserEntityId == CurrentUserStatic.User.Id).OrderBy(l => l.Date).Select(l => 
                    new DateOnly(
                        l.Date.Year, 
                        l.Date.Month,
                        l.Date.Day)).ToListAsync();

            if(daysList != null)
            {
                return daysList;
            }

            return new();
        }

        public async Task<List<DailyTaskEntity>> GetDailyTasksByListId(int listId)
        {
            List<DailyTaskEntity>? currentDailyTasks =
                await _context.DailyTasks.Where(d => 
                d.DailyTasksListId == listId).ToListAsync();

            if(currentDailyTasks != null)
            {
                return currentDailyTasks;
            }

            return new();
        }
    }
}
