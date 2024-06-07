using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<RealLifeTask> RealLifeTask { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RealLifeTask>()
                .HasOne(rl => rl.Category)
                .WithMany(c => c.RealLifeTasks)
                .HasForeignKey(rl => rl.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Use Restrict or NoAction to prevent cascading deletes

            base.OnModelCreating(modelBuilder);
        }
    }
}
