using DailyPlanner.Models;
using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IDailyTaskRepository : 
        IBaseRepository<DailyTaskModel, DailyTaskModel>
    {

    }
}
