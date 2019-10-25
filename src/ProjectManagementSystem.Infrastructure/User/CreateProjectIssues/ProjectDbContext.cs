using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.DatabaseMigrations.Entities;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        internal DbSet<Project> Projects { get; set; }
        internal DbSet<Tracker> Trackers { get; set; }
        internal DbSet<Issue> Issues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Project>(builder =>
            {
                builder.ToTable("Project");
            });
            
            modelBuilder.Entity<Tracker>(builder =>
            {
                builder.ToTable("Tracker");
            });
            
            modelBuilder.Entity<Issue>(builder =>
            {
                builder.ToTable("Issue");
            });
        }
    }
}