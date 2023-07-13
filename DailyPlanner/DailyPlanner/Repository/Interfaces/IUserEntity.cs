using DailyPlanner.Repository.Entitites;

namespace DailyPlanner.Repository.Interfaces
{
    public interface IUserEntity : IEntity
    {
        string Login { get; set; }
        string Password { get; set; }
        List<DailyTasksListEntity>? DailyTasksLists { get; set; }
        List<GeneralTaskEntity>? GeneralTasks { get; set; }
    }
}
