using DailyPlanner.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Repository.Entitites;

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
    }
}
