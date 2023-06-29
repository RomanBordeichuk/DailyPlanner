using DailyPlanner.Models;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository.Repos
{
    public class DailyTaskRepository : IDailyTaskRepository
    {
        private readonly AppDbContext _context;

        public DailyTaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DailyTaskModel> AddAsync(DailyTaskModel dailyTask)
        {
            DailyTaskEntity dailyTaskEntity = new(dailyTask); 

            await _context.DailyTasks.AddAsync(dailyTaskEntity);
            await _context.SaveChangesAsync();

            return dailyTask;
        }

        public async Task<DailyTaskModel> GetByIdAsync(int id)
        {
            DailyTaskEntity? dailyTaskEntity =
                await _context.DailyTasks.FirstOrDefaultAsync(d => d.Id == id);

            if(dailyTaskEntity != null)
            {
                DailyTaskModel dailyTaskModel = new(dailyTaskEntity);

                return dailyTaskModel;
            }

            throw new Exception("Daily Task hand't found");
        }

        public async Task<List<DailyTaskModel>> GetAllAsync()
        {
            List<DailyTaskEntity> dailyTasks = 
                await _context.DailyTasks.ToListAsync();

            List<DailyTaskModel> dailyTaskModels = new();

            foreach(DailyTaskEntity dailyTask in dailyTasks)
            {
                DailyTaskModel dailyTaskModel = new(dailyTask);
                dailyTaskModels.Add(dailyTaskModel);
            }

            return dailyTaskModels;
        }

        public async Task<DailyTaskModel> DeleteByIdAsync(int id)
        {
            DailyTaskEntity? dailyTask =
                await _context.DailyTasks.FirstOrDefaultAsync(d => d.Id == id);

            if(dailyTask != null)
            {
                _context.DailyTasks.Remove(dailyTask);
                await _context.SaveChangesAsync();

                DailyTaskModel dailyTaskModel = new(dailyTask);
                return dailyTaskModel;
            }

            throw new Exception("Daily Task had't found");
        }

        public async Task<DailyTaskModel> UpdateByIdAsync(
            int id, DailyTaskModel dailyTask)
        {
            DailyTaskEntity? dbDailyTask =
                await _context.DailyTasks.FirstOrDefaultAsync(d => d.Id == id);

            if(dbDailyTask != null)
            {
                DailyTaskEntity dailyTaskEntity = new(dailyTask);
                dbDailyTask = dailyTaskEntity;

                await _context.SaveChangesAsync();

                return dailyTask;
            }

            throw new Exception("Daily Task had't found");
        }
    }
}
