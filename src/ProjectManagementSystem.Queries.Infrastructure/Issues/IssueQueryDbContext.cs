using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Issues;

public sealed class IssueQueryDbContext : DbContext
{
    public IssueQueryDbContext(DbContextOptions<IssueQueryDbContext> options) : base(options) { }

    internal DbSet<Issue> Issues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.ProjectId); ;
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
        });
            
        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(o => o.IssueId);
            builder.Property(o => o.Title)
                .IsRequired();
            builder.Property(o => o.Description)
                .IsRequired(false);
            builder.Property(o => o.State)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property(o => o.DueDate)
                .IsRequired(false);

            builder.HasOne(o => o.Project)
                .WithMany()
                .HasForeignKey(o => o.ProjectId)
                .HasPrincipalKey(o => o.ProjectId);
            
            builder.HasOne(o => o.Author)
                .WithMany()
                .HasForeignKey(o => o.AuthorId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasOne(o => o.UserWhoClosed)
                .WithMany()
                .HasForeignKey(o => o.UserIdWhoClosed)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.Assignees)
                .WithMany(o => o.Issues)
                .UsingEntity<IssueAssignee>(
                    l => l.HasOne<User>().WithMany().HasForeignKey(e => e.AssigneeId),
                    r => r.HasOne<Issue>().WithMany().HasForeignKey(e => e.IssueId));

            // builder.HasMany(o => o.TimeEntries)
            //     .WithOne(o => o.Issue)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
            //
            // builder.HasMany(o => o.Comments)
            //     .WithOne(o => o.Issue)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
            //
            // builder.HasMany(o => o.IssueAssignees)
            //     .WithOne(o => o.Issue)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
            //
            // builder.HasMany(o => o.IssueLabels)
            //     .WithOne(o => o.Issue)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
            //
            // builder.HasMany(o => o.IssueUserReactions)
            //     .WithOne(o => o.Issue)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
        });
    }
}