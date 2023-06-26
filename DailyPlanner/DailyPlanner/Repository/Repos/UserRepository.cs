using DailyPlanner.Models;
using DailyPlanner.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }
        public async Task<User> GetByIdAsync(int id)
        {
            User? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if(user != null)
            {
                return user;
            }

            throw new Exception("User not found");
        }
        public async Task<List<User>> GetAllAsync()
        {
            List<User>? users = await _context.Users.ToListAsync();

            if(users != null)
            {
                return users;
            }

            throw new Exception("Users not found");
        }
        public async Task<User> UpdateByIdAsync(int id, User user)
        {
            var oldUser = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(oldUser != null)
            {
                user = oldUser;
                await _context.SaveChangesAsync();
            }

            throw new Exception("Incorrect id");
        }
        public async Task<User> DeleteByIdAsync(int id)
        {
            User? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }

            throw new Exception("Incorrect id");
        }
    }
}
