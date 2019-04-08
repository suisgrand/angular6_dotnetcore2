using Microsoft.EntityFrameworkCore;
using proj.Models;

namespace proj.Persistence
{
    public class TaskDbContext : DbContext
    {

        public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
        {

        }

        public DbSet<Login> Logins { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }

    }
}