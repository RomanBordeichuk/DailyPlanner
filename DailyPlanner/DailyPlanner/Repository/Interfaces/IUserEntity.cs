namespace DailyPlanner.Repository.Interfaces
{
    public interface IUserEntity : IEntity
    {
        string Login { get; set; }
        string Password { get; set; }
    }
}
