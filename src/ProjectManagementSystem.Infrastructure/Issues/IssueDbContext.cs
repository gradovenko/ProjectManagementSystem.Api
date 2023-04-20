using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class IssueDbContext : DbContext
{
    public IssueDbContext(DbContextOptions<IssueDbContext> options) : base(options) { }

    internal DbSet<Project> Projects { get; set; }
    internal DbSet<User> Users { get; set; }
    internal DbSet<Issue> Issues { get; set; }
    internal DbSet<Reaction> Reactions { get; set; }
    internal DbSet<Label> Labels { get; set; }
    internal DbSet<IssueAssignee> IssueAssignees { get; set; }
    internal DbSet<IssueLabel> IssueLabels { get; set; }
    internal DbSet<IssueUserReaction> IssueUserReactions { get; set; }

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
                .HasColumnName("IssueId")
                .ValueGeneratedNever();
            builder.Property(i => i.Title)
                .IsRequired();
            builder.Property(i => i.Description)
                .IsRequired(false);
            builder.Property(i => i.State)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(i => i.CreateDate)
                .IsRequired();
            builder.Property(i => i.UpdateDate)
                .IsRequired();
            builder.Property(i => i.DueDate)
                .IsRequired(false);
            builder.Property(i => i.UserIdWhoClosed)
                .IsRequired(false);
            builder.Property(i => i.ProjectId)
                .IsRequired();
            builder.Property(i => i.AuthorId)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();

            // builder.HasOne(o => o.Project)
            //     .WithMany(o => o.Issues)
            //     .HasForeignKey(o => o.ProjectId)
            //     .HasPrincipalKey(o => o.ProjectId);
            //
            // builder.HasOne(o => o.Author)
            //     .WithMany(o => o.CreatedIssues)
            //     .HasForeignKey(o => o.AuthorId)
            //     .HasPrincipalKey(o => o.UserId);
            //
            // builder.HasOne(o => o.UserWhoClosed)
            //     .WithMany(o => o.ClosedIssues)
            //     .HasForeignKey(o => o.UserIdWhoClosed)
            //     .HasPrincipalKey(o => o.UserId);

            // builder.HasMany(o => o.IssueAssignees)
            //     .WithOne()
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.Id);
            //
            // builder.HasMany(o => o.IssueLabels)
            //     .WithOne(o => o.Issue)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.Id);
            //
            // builder.HasMany(o => o.IssueUserReactions)
            //     .WithOne()
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.Id);
        });
        
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("UserId")
                .ValueGeneratedNever();
        });
        
        modelBuilder.Entity<Label>(builder =>
        {
            builder.ToTable("Label");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasColumnName("LabelId")
                .ValueGeneratedNever();
        });
        
        modelBuilder.Entity<Reaction>(builder =>
        {
            builder.ToTable("Reaction");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .HasColumnName("ReactionId")
                .ValueGeneratedNever();
        });
        
        modelBuilder.Entity<IssueAssignee>(builder =>
        {
            builder.ToTable("IssueAssignee");
            builder.HasKey(o => new { o.IssueId, o.AssigneeId });

            // builder.HasOne(o => o.Issue)
            //     .WithMany(o => o.IssueAssignees)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
            //
            // builder.HasOne(o => o.Assignee)
            //     .WithMany(o => o.IssueAssignees)
            //     .HasForeignKey(o => o.AssigneeId)
            //     .HasPrincipalKey(o => o.UserId);
        });
        
        modelBuilder.Entity<IssueLabel>(builder =>
        {
            builder.ToTable("IssueLabel");
            builder.HasKey(o => new { o.IssueId, o.LabelId });

            // builder.HasOne(o => o.Issue)
            //     .WithMany(o => o.IssueLabels)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.Id);
            //
            // builder.HasOne(o => o.Label)
            //     .WithMany(o => o.IssueLabels)
            //     .HasForeignKey(o => o.LabelId)
            //     .HasPrincipalKey(o => o.Id);
        });
        
        modelBuilder.Entity<IssueUserReaction>(builder =>
        {
            builder.ToTable("IssueUserReaction");
            builder.HasKey(o => new {o.IssueId, o.UserId, o.ReactionId});

            // builder.HasOne(o => o.Issue)
            //     .WithMany(o => o.IssueUserReactions)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);
            //
            // builder.HasOne(o => o.User)
            //     .WithMany(o => o.IssueUserReactions)
            //     .HasForeignKey(o => o.UserId)
            //     .HasPrincipalKey(o => o.UserId);
            //
            // builder.HasOne(o => o.Reaction)
            //     .WithMany(o => o.IssueUserReactions)
            //     .HasForeignKey(o => o.ReactionId)
            //     .HasPrincipalKey(o => o.ReactionId);
        });
    }
}