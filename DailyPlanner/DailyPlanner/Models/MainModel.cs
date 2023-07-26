using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;
using DailyPlanner.StaticClasses;

namespace DailyPlanner.Models
{
    public class MainModel
    {
        public string Login { get; set; } = CurrentUserStatic.User?.Login ?? "New Account";
        public string MotivationalQuote { get; set; } = MotivationalQuoteStatic.Quote;
        public GeneralTaskEntity ClosesGeneralTask { get; set; } = new();

        public IUserRepository? UserRepository;
        public IDailyTasksRepository? DailyTasksRepository;
        public IGeneralTasksRepository? GeneralTasksRepository;

        public async Task<string> GetFromDbMotivationalQuote()
        {
            if(UserRepository == null)
            {
                throw new Exception("UserRepository not found");
            }
            if(CurrentUserStatic.User == null)
            {
                throw new Exception("User not found");
            }

            MotivationalQuote = await UserRepository.GetMotivationalQuoteByUserId(
                CurrentUserStatic.User.Id);

            return MotivationalQuote;
        }

        public async Task<string> SaveChangedMotivationalQuote()
        {
            if(UserRepository == null)
            {
                throw new Exception("UserRepository not found");
            }
            if(CurrentUserStatic.User == null)
            {
                throw new Exception("User not found");
            }

            await UserRepository.UpdateQuoteById(CurrentUserStatic.User.Id, MotivationalQuote);

            return MotivationalQuote;
        }

        public async Task<GeneralTaskEntity> GetFromDbClosestTask()
        {
            if(GeneralTasksRepository == null)
            {
                throw new Exception("GeneralTasksRepository not found");
            }
            if(CurrentUserStatic.User == null)
            {
                throw new Exception("User not found");
            }

            ClosesGeneralTask =  
                await GeneralTasksRepository.GetClosesGeneralTaskByUserId(
                    CurrentUserStatic.User.Id);

            return ClosesGeneralTask;
        }
    }
}
