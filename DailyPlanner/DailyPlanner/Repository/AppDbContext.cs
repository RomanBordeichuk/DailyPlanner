using DailyPlanner.Repository.Entitites;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<DailyTaskEntity> DailyTasks { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
    }
}
