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
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .HasColumnName("ProjectId");
            });

            modelBuilder.Entity<Tracker>(builder =>
            {
                builder.ToTable("Tracker");
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Id)
                    .HasColumnName("TrackerId");
            });

            modelBuilder.Entity<IssueStatus>(builder =>
            {
                builder.ToTable("IssueStatus");
                builder.HasKey(@is => @is.Id);
                builder.Property(@is => @is.Id)
                    .HasColumnName("IssueStatusId");
            });
            
            modelBuilder.Entity<IssuePriority>(builder =>
            {
                builder.ToTable("IssuePriority");
                builder.HasKey(ip => ip.Id);
                builder.Property(ip => ip.Id)
                    .HasColumnName("IssuePriorityId");
            });
            
            modelBuilder.Entity<Domain.User.CreateProjectIssues.User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id)
                    .HasColumnName("UserId");
            });
            
            modelBuilder.Entity<Issue>(builder =>
            {
                builder.ToTable("Issue");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.Id)
                    .HasColumnName("IssueId")
                    .ValueGeneratedNever();
                builder.Property(i => i.Title)
                    .IsRequired();
                builder.Property(i => i.Description)
                    .IsRequired();
                builder.Property(i => i.CreateDate)
                    .IsRequired();
                builder.Property(i => i.StartDate);
                builder.Property(i => i.DueDate);
                builder.Property(i => i.TrackerId)
                    .IsRequired();
                builder.Property(i => i.StatusId)
                    .IsRequired();
                builder.Property(i => i.PriorityId)
                    .IsRequired();
                builder.Property(i => i.AuthorId)
                    .IsRequired();
                builder.Property(i => i.AssigneeId);
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
                builder.HasOne(i => i.Tracker)
                    .WithMany()
                    .HasForeignKey(i => i.TrackerId)
                    .HasPrincipalKey(t => t.Id);
                builder.HasOne(i => i.Status)
                    .WithMany()
                    .HasForeignKey(i => i.StatusId)
                    .HasPrincipalKey(@is => @is.Id);
                builder.HasOne(i => i.Priority)
                    .WithMany()
                    .HasForeignKey(i => i.PriorityId)
                    .HasPrincipalKey(ip => ip.Id);
                builder.HasOne(i => i.Author)
                    .WithMany()
                    .HasForeignKey(i => i.AuthorId)
                    .HasPrincipalKey(a => a.Id);
                builder.HasOne(i => i.Assignee)
                    .WithMany()
                    .HasForeignKey(i => i.AssigneeId)
                    .HasPrincipalKey(p => p.Id);
            });
        }
    }
}