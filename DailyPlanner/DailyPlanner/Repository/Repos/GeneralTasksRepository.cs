using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Repository.Repos
{
    public class GeneralTasksRepository : IGeneralTasksRepository
    {
        public Task<GeneralTaskEntity> AddAsync(GeneralTaskEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralTaskEntity> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GeneralTaskEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GeneralTaskEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralTaskEntity> UpdateByIdAsync(int id, GeneralTaskEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
