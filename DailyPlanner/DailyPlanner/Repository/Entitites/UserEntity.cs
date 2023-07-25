namespace DailyPlanner.Repository.Entitites
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public List<DailyTasksListEntity>? DailyTasksLists { get; set; }
        public List<GeneralTaskEntity>? GeneralTasks { get; set; }

        public UserEntity(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
