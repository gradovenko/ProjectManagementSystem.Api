using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.CreateProjectIssues;

namespace ProjectManagementSystem.Infrastructure.User.CreateProjectIssues
{
    public sealed class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

        internal DbSet<Project> Projects { get; set; }
        internal DbSet<Tracker> Trackers { get; set; }
        internal DbSet<IssueStatus> IssueStatuses { get; set; }
        internal DbSet<IssuePriority> IssuePriorities { get; set; }
        internal DbSet<Domain.User.CreateProjectIssues.User> Users { get; set; }
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

            modelBuilder.Entity<IssueStatus>(builder =>
            {
                builder.ToTable("IssueStatus");
            });
            
            modelBuilder.Entity<IssuePriority>(builder =>
            {
                builder.ToTable("IssuePriority");
            });
            
            modelBuilder.Entity<Domain.User.CreateProjectIssues.User>(builder =>
            {
                builder.ToTable("User");
            });
            
            modelBuilder.Entity<Issue>(builder =>
            {
                builder.ToTable("Issue");
            });
        }
    }
}