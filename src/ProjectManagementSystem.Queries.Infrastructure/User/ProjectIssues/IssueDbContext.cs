using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues;

public sealed class IssueDbContext : DbContext
{
    public IssueDbContext(DbContextOptions<IssueDbContext> options) : base(options) { }

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
            builder.Property(t => t.Name);
        });

        modelBuilder.Entity<IssueStatus>(builder =>
        {
            builder.ToTable("IssueStatus");
            builder.HasKey(@is => @is.Id);
            builder.Property(@is => @is.Id)
                .HasColumnName("IssueStatusId");
            builder.Property(@is => @is.Name);
        });

        modelBuilder.Entity<IssuePriority>(builder =>
        {
            builder.ToTable("IssuePriority");
            builder.HasKey(ip => ip.Id);
            builder.Property(ip => ip.Id)
                .HasColumnName("IssuePriorityId");
            builder.Property(ip => ip.Name);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("UserId");
            builder.Property(u => u.Name);
        });

        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("IssueId");
            builder.Property(i => i.Number);
            builder.Property(i => i.Title);
            builder.Property(i => i.Description);
            builder.Property(i => i.CreateDate);
            builder.Property(i => i.StartDate);
            builder.Property(i => i.DueDate);
            builder.Property(i => i.TrackerId);
            builder.Property(i => i.StatusId);
            builder.Property(i => i.PriorityId);
            builder.Property(i => i.AuthorId);
            builder.Property(i => i.AssigneeId);
            builder.HasOne(i => i.Project)
                .WithMany()
                .HasForeignKey(i => i.ProjectId)
                .HasPrincipalKey(p => p.Id);
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