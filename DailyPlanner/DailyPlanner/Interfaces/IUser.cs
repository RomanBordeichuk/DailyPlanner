namespace DailyPlanner.Interfaces
{
    public interface IUser : IModel
    {   
        string? Login { get; set; }
        string? Password { get; set; }
    }
}
