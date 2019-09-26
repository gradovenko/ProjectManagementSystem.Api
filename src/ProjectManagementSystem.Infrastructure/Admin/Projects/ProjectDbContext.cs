using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.Projects;

namespace ProjectManagementSystem.Infrastructure.Admin.Projects
{
    public sealed class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
        
        internal DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>(builder =>
            {
                
            });
        }
    }
}