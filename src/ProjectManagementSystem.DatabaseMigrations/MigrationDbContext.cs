using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.DatabaseMigrations.Entities;

namespace ProjectManagementSystem.DatabaseMigrations;

public sealed class MigrationDbContext : DbContext
{ 
    public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .ValueGeneratedNever();
            builder.Property(u => u.Name)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(u => u.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(u => u.PasswordHash)
                .HasMaxLength(1024)
                .IsRequired();
            builder.Property(u => u.FirstName)
                .IsRequired();
            builder.Property(u => u.LastName)
                .IsRequired();
            builder.Property(u => u.Role)
                .IsRequired();
            builder.Property(u => u.CreateDate)
                .IsRequired();
            builder.Property(u => u.UpdateDate);
            builder.Property(u => u.Status)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(u => u.ConcurrencyStamp)
                .IsConcurrencyToken();
            builder.HasIndex(u => u.Name)
                .IsUnique();
            builder.HasIndex(u => u.Email)
                .IsUnique();
                
            builder.HasData(new User
            {
                UserId = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                Name = "Admin",
                Email = "admin@projectms.local",
                PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                FirstName = "Admin",
                LastName = "Admin",
                Role = "Admin",
                CreateDate = DateTime.UnixEpoch,
                Status = "Active",
                ConcurrencyStamp = Guid.NewGuid()
            });
        });
            
        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(rt => rt.RefreshTokenId);
            builder.Property(rt => rt.RefreshTokenId)
                .ValueGeneratedNever();
            builder.Property(rt => rt.ExpireDate)
                .IsRequired();
            builder.Property(rt => rt.UserId)
                .IsRequired();
        });
            
        modelBuilder.Entity<IssuePriority>(builder =>
        {
            builder.ToTable("IssuePriority");
            builder.HasKey(ip => ip.IssuePriorityId);
            builder.Property(ip => ip.IssuePriorityId)
                .ValueGeneratedNever();
            builder.Property(ip => ip.Name)
                .IsRequired();
            builder.Property(ip => ip.IsActive)
                .IsRequired();
        });
            
        modelBuilder.Entity<IssueStatus>(builder =>
        {
            builder.ToTable("IssueStatus");
            builder.HasKey(@is => @is.IssueStatusId);
            builder.Property(@is => @is.IssueStatusId)
                .ValueGeneratedNever();
            builder.Property(@is => @is.Name)
                .IsRequired();
            builder.Property(@is => @is.IsActive)
                .IsRequired();
        });
            
        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId)
                .ValueGeneratedNever();
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p => p.IsPrivate)
                .IsRequired();
            builder.Property(p => p.Status)
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired();
            builder.Property(u => u.ConcurrencyStamp)
                .IsConcurrencyToken();
        });
            
        modelBuilder.Entity<Tracker>(builder =>
        {
            builder.ToTable("Tracker");
            builder.HasKey(t => t.TrackerId);
            builder.Property(t => t.TrackerId)
                .ValueGeneratedNever();
            builder.Property(t => t.Name)
                .IsRequired();
            builder.Property(t => t.ConcurrencyStamp)
                .IsConcurrencyToken();
        });
            
        modelBuilder.Entity<ProjectTracker>(builder =>
        {
            builder.ToTable("ProjectTracker");
            builder.HasKey(pt => new { pt.ProjectId, pt.TrackerId });
            builder.HasOne(pt => pt.Project)
                .WithMany()
                .HasForeignKey(pt => pt.ProjectId)
                .HasPrincipalKey(p => p.ProjectId);
            builder.HasOne(pt => pt.Tracker)
                .WithMany()
                .HasForeignKey(pt => pt.TrackerId)
                .HasPrincipalKey(t => t.TrackerId);
        });
            
        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.IssueId);
            builder.Property(i => i.IssueId)
                .ValueGeneratedNever();
            builder.Property(i => i.Number)
                .IsRequired();
            builder.Property(i => i.Title)
                .IsRequired();
            builder.Property(i => i.Description)
                .IsRequired();
            builder.Property(i => i.CreateDate)
                .IsRequired();
            builder.Property(i => i.UpdateDate);
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
            builder.Property(i => i.ConcurrencyStamp)
                .IsConcurrencyToken();
            builder.HasOne(i => i.Project)
                .WithMany()
                .HasForeignKey(i => i.ProjectId)
                .HasPrincipalKey(p => p.ProjectId);
            builder.HasOne(i => i.Tracker)
                .WithMany()
                .HasForeignKey(i => i.TrackerId)
                .HasPrincipalKey(t => t.TrackerId);
            builder.HasOne(i => i.Status)
                .WithMany()
                .HasForeignKey(i => i.StatusId)
                .HasPrincipalKey(@is => @is.IssueStatusId);
            builder.HasOne(i => i.Priority)
                .WithMany()
                .HasForeignKey(i => i.PriorityId)
                .HasPrincipalKey(ip => ip.IssuePriorityId);
            builder.HasOne(i => i.Author)
                .WithMany()
                .HasForeignKey(i => i.AuthorId)
                .HasPrincipalKey(a => a.UserId);
            builder.HasOne(i => i.Assignee)
                .WithMany()
                .HasForeignKey(i => i.AssigneeId)
                .HasPrincipalKey(p => p.UserId);
        });
            
        modelBuilder.Entity<TimeEntryActivity>(builder =>
        {
            builder.ToTable("TimeEntryActivity");
            builder.HasKey(tea => tea.TimeEntryActivityId);
            builder.Property(tea => tea.TimeEntryActivityId)
                .ValueGeneratedNever();
            builder.Property(tea => tea.Name)
                .IsRequired();
            builder.Property(tea => tea.IsActive)
                .IsRequired();
            builder.Property(tea => tea.ConcurrencyStamp)
                .IsConcurrencyToken();
        });
            
        modelBuilder.Entity<TimeEntry>(builder =>
        {
            builder.ToTable("TimeEntry");
            builder.HasKey(te => te.TimeEntryId);
            builder.Property(te => te.TimeEntryId)
                .ValueGeneratedNever();
            builder.Property(te => te.Hours)
                .IsRequired();
            builder.Property(te => te.Description)
                .IsRequired();
            builder.Property(te => te.DueDate)
                .IsRequired();
            builder.Property(te => te.CreateDate)
                .IsRequired();
            builder.Property(te => te.UpdateDate);
            builder.Property(te => te.ProjectId)
                .IsRequired();
            builder.Property(te => te.IssueId)
                .IsRequired();
            builder.Property(te => te.UserId)
                .IsRequired();
            builder.Property(te => te.ActivityId)
                .IsRequired();
            builder.Property(te => te.ConcurrencyStamp)
                .IsConcurrencyToken();
            builder.HasOne(te => te.Project)
                .WithMany()
                .HasForeignKey(te => te.ProjectId)
                .HasPrincipalKey(p => p.ProjectId);
            builder.HasOne(te => te.Issue)
                .WithMany()
                .HasForeignKey(te => te.IssueId)
                .HasPrincipalKey(p => p.IssueId);
            builder.HasOne(te => te.User)
                .WithMany()
                .HasForeignKey(te => te.UserId)
                .HasPrincipalKey(p => p.UserId);
            builder.HasOne(te => te.Activity)
                .WithMany()
                .HasForeignKey(te => te.ActivityId)
                .HasPrincipalKey(p => p.TimeEntryActivityId);
        });
    }
}