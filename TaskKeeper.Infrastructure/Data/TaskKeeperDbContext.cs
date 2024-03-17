using Microsoft.EntityFrameworkCore;
using TaskKeeper.Domain;

namespace TaskKeeper.Persistence.Data
{
    public class TaskKeeperDbContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public TaskKeeperDbContext(DbContextOptions<TaskKeeperDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}