using DailyPlanner.Repository.Hashing;
using DailyPlanner.Models;
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

        public async Task<UserViewModel> AddAsync(UserModel user)
        {
            UserEntity userEntity = new(user);

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            UserViewModel userView = new(userEntity);

            return userView;
        }
        public async Task<UserViewModel> GetByIdAsync(int id)
        {
            UserEntity? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if(user != null)
            {
                UserViewModel userView = new(user);

                return userView;
            }

            throw new Exception("User not found");
        }
        public async Task<List<UserViewModel>> GetAllAsync()
        {
            List<UserEntity>? users = await _context.Users.ToListAsync();

            if(users != null)
            {
                List<UserViewModel> userViews = new();

                foreach(UserEntity userEntity in users)
                {
                    userViews.Add(new(userEntity));
                }

                return userViews;
            }

            throw new Exception("Users not found");
        }
        public async Task<UserViewModel> UpdateByIdAsync(int id, UserModel user)
        {
            var oldUser = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(oldUser != null)
            {
                UserEntity userEntity = new(user);

                oldUser = userEntity;

                await _context.SaveChangesAsync();
            }

            throw new Exception("Incorrect id");
        }
        public async Task<UserViewModel> DeleteByIdAsync(int id)
        {
            UserEntity? user = 
                await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user != null)
            {
                _context.Users.Remove(user);

                UserViewModel userView = new(user);

                await _context.SaveChangesAsync();

                return userView;
            }

            throw new Exception("Incorrect id");
        }
        public async Task<bool> ContainsAsync(UserModel user)
        {
            UserEntity userEntity = new(user);

            UserEntity? dbUserEntity = 
                await _context.Users.FirstOrDefaultAsync(u => 
                u.Login == userEntity.Login && 
                u.Password == userEntity.Password);

            if(dbUserEntity != null)
            {
                return true;
            }

            return false;
        }
    }
}
