using Microsoft.EntityFrameworkCore;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Interfaces;

namespace DailyPlanner.Repository.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ContainsAsync(UserEntity user)
        {
            UserEntity? dbUser = 
                await _context.Users.FirstOrDefaultAsync(u => 
                u.Login == user.Login && 
                u.Password == user.Password);

            if(dbUser != null)
            {
                return true;
            }

            return false;
        }

        public async Task<UserEntity> GetAsync(UserEntity user)
        {
            UserEntity? userEntity =
                await _context.Users.FirstOrDefaultAsync(u => 
                u.Login == user.Login &&
                u.Password == user.Password);

            if(userEntity != null)
            {
                return userEntity;
            }

            throw new Exception("User not found");
        }

        public async Task<string> GetMotivationalQuoteByUserId(int userId)
        {
            UserEntity? dbUser =
                await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if(dbUser == null)
            {
                throw new Exception("dbUser not found");
            }

            return dbUser.MotivationalQuote;
        }

        public async Task<string> UpdateQuoteById(int userId, string quote)
        {
            UserEntity? dbUser = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if(dbUser == null)
            {
                throw new Exception("dbUser not found");
            }

            dbUser.MotivationalQuote = quote;

            await _context.SaveChangesAsync();

            return quote;
        }
    }
}
