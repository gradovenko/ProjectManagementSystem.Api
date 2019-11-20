using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.User.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.User.TimeEntries
{
    public sealed class TimeEntryDbContext : DbContext
    {
        public TimeEntryDbContext(DbContextOptions<TimeEntryDbContext> options) : base(options) { }

        internal DbSet<Project> Projects { get; set; }
        internal DbSet<Issue> Issues { get; set; }
        internal DbSet<Domain.User.TimeEntries.User> Users { get; set; }
        internal DbSet<TimeEntryActivity> TimeEntryActivities { get; set; }
        internal DbSet<TimeEntry> TimeEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Project>(builder =>
            {
                builder.ToTable("Project");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id).IsRequired();
            });
            
            modelBuilder.Entity<Issue>(builder =>
            {
                builder.ToTable("Issue");
                builder.HasKey(i => i.Id);
                builder.Property(i => i.Id)
                    .IsRequired();
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });

            modelBuilder.Entity<Domain.User.TimeEntries.User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id)
                    .IsRequired();
            });
            
            modelBuilder.Entity<TimeEntryActivity>(builder =>
            {
                builder.ToTable("TimeEntryActivity");
                builder.HasKey(tea => tea.Id);
                builder.Property(tea => tea.Id)
                    .IsRequired();
            });
            
            modelBuilder.Entity<TimeEntry>(builder =>
            {
                builder.ToTable("TimeEntry");
                builder.HasKey(te => te.Id);
                builder.Property(te => te.Id)
                    .ValueGeneratedNever();
                builder.Property(te => te.Hours)
                    .IsRequired();
                builder.Property(te => te.Description)
                    .IsRequired();
                builder.Property(te => te.DueDate)
                    .IsRequired();
                builder.Property(te => te.CreateDate)
                    .IsRequired();
                builder.Property(te => te.ProjectId)
                    .IsRequired();
                builder.Property(te => te.UserId)
                    .IsRequired();
                builder.Property(te => te.ActivityId)
                    .IsRequired();
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
                builder.HasOne(te => te.Project)
                    .WithMany()
                    .HasForeignKey(te => te.ProjectId)
                    .HasPrincipalKey(p => p.Id);
                builder.HasOne(te => te.User)
                    .WithMany()
                    .HasForeignKey(te => te.UserId)
                    .HasPrincipalKey(p => p.Id);
                builder.HasOne(te => te.Activity)
                    .WithMany()
                    .HasForeignKey(te => te.ActivityId)
                    .HasPrincipalKey(p => p.Id);
            });
        }
    }
}