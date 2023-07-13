namespace DailyPlanner.Interfaces
{
    public interface IHomePageModel : IModel
    {
        string UserLogin { get; set; }
        string UserPassword { get; set; }
        bool HasUserInDb { get; set; }
    }
}
