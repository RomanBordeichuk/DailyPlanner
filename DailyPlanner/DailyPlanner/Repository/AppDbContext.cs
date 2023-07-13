using DailyPlanner.Repository.Entitites;
using Microsoft.EntityFrameworkCore;

namespace DailyPlanner.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<DailyTaskEntity> DailyTasks { get; set; } = null!;
        public DbSet<DailyTasksListEntity> DailyTasksLists { get; set; } = null!;
        public DbSet<GeneralTaskEntity> GeneralTasks { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyTaskEntity>()
                .HasOne(d => d.DailyTasksList)
                .WithMany(l => l.DailyTasks);

            modelBuilder.Entity<DailyTasksListEntity>()
                .HasOne(l => l.UserEntity)
                .WithMany(u => u.DailyTasksLists);

            modelBuilder.Entity<GeneralTaskEntity>()
                .HasOne(g => g.UserEntity)
                .WithMany(u => u.GeneralTasks);
        }
    }
}
