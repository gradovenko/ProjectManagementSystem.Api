using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.TimeEntries;

public sealed class TimeEntryDbContext : DbContext
{
    public TimeEntryDbContext(DbContextOptions<TimeEntryDbContext> options) : base(options) { }

    internal DbSet<TimeEntry> TimeEntries { get; set; }

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
            
        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("IssueId");
            builder.Property(i => i.Number);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("UserId");
            builder.Property(u => u.Name);
        });
            
        modelBuilder.Entity<TimeEntryActivity>(builder =>
        {
            builder.ToTable("TimeEntryActivity");
            builder.HasKey(tea => tea.Id);
            builder.Property(tea => tea.Id)
                .HasColumnName("TimeEntryActivityId");
            builder.Property(tea => tea.Name);
        }); 

        modelBuilder.Entity<TimeEntry>(builder =>
        {
            builder.ToTable("TimeEntry");
            builder.HasKey(te => te.Id);
            builder.Property(te => te.Id)
                .HasColumnName("TimeEntryId");
            builder.Property(te => te.Hours);
            builder.Property(te => te.Description);
            builder.Property(te => te.DueDate);
            builder.Property(te => te.CreateDate);
            builder.Property(te => te.ProjectId);
            builder.Property(te => te.UserId);
            builder.Property(te => te.ActivityId);
            builder.HasOne(te => te.Project)
                .WithMany()
                .HasForeignKey(te => te.ProjectId)
                .HasPrincipalKey(p => p.Id);
            builder.HasOne(te => te.Issue)
                .WithMany()
                .HasForeignKey(te => te.IssueId)
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