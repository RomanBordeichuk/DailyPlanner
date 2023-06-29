namespace DailyPlanner.Interfaces
{
    public interface IUserModel : IModel
    {   
        string Login { get; set; }
        string Password { get; set; }
    }
}
