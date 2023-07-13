using DailyPlanner.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using DailyPlanner.Repository.Entitites;
using DailyPlanner.Repository.Hashing;

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
        public async Task<UserEntity> GetByIdAsync(int id)
        {
            UserEntity? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if(user != null)
            {
                return user;
            }

            throw new Exception("User not found");
        }
        public async Task<List<UserEntity>> GetAllAsync()
        {
            List<UserEntity>? users = await _context.Users.ToListAsync();

            if(users != null)
            {
                return users;
            }

            throw new Exception("Users not found");
        }
        public async Task<UserEntity> UpdateByIdAsync(int id, UserEntity user)
        {
            var oldUser = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(oldUser != null)
            {
                oldUser = user;
                await _context.SaveChangesAsync();

                return user;
            }

            throw new Exception("Incorrect id");
        }
        public async Task<UserEntity> DeleteByIdAsync(int id)
        {
            UserEntity? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return user;
            }

            throw new Exception("Incorrect id");
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
