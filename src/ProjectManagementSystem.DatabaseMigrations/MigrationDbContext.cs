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
            builder.HasKey(o => o.UserId);
            builder.Property(o => o.UserId)
                .ValueGeneratedNever();
            builder.Property(o => o.Name)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(o => o.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(o => o.PasswordHash)
                .HasMaxLength(1024)
                .IsRequired();
            builder.Property(o => o.Role)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property(o => o.State)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(o => o.ConcurrencyToken)
                .IsConcurrencyToken();

            builder.HasMany(o => o.IssueAssignees)
                .WithOne(o => o.Assignee)
                .HasForeignKey(o => o.AssigneeId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.CreatedIssues)
                .WithOne(o => o.Author)
                .HasForeignKey(o => o.AuthorId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.ClosedIssues)
                .WithOne(o => o.UserWhoClosed)
                .HasForeignKey(o => o.UserIdWhoClosed)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.TimeEntries)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.IssueUserReactions)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.CommentUserReactions)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasData(new User
            {
                UserId = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                Name = "Admin",
                Email = "admin@projectms.local",
                PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                Role = "Admin",
                CreateDate = DateTime.UnixEpoch,
                UpdateDate = DateTime.UnixEpoch,
                State = "Active",
                ConcurrencyToken = Guid.NewGuid()
            });
        });
            
        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(o => o.RefreshTokenId);
            builder.Property(o => o.RefreshTokenId)
                .ValueGeneratedNever();
            builder.Property(o => o.ExpireDate)
                .IsRequired();
            builder.Property(o => o.UserId)
                .IsRequired()
                .HasMaxLength(32);
        });

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(o => o.ProjectId);
            builder.Property(o => o.ProjectId)
                .ValueGeneratedNever();
            builder.Property(o => o.Name)
                .IsRequired();
            builder.Property(o => o.Description)
                .IsRequired();
            builder.Property(o => o.Path)
                .IsRequired();
            builder.Property(o => o.Visibility)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property(o => o.ConcurrencyToken)
                .IsConcurrencyToken();
            
            builder.HasMany(o => o.Issues)
                .WithOne(o => o.Project)
                .HasForeignKey(o => o.ProjectId)
                .HasPrincipalKey(o => o.ProjectId);
        });

        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(o => o.IssueId);
            builder.Property(o => o.IssueId)
                .ValueGeneratedNever();
            builder.Property(o => o.Title)
                .IsRequired();
            builder.Property(o => o.Description)
                .IsRequired(false);
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property(o => o.DueDate)
                .IsRequired(false);
            builder.Property(o => o.ConcurrencyToken)
                .IsConcurrencyToken();

            builder.HasOne(o => o.Project)
                .WithMany(o => o.Issues)
                .HasForeignKey(o => o.ProjectId)
                .HasPrincipalKey(o => o.ProjectId);
            
            builder.HasOne(o => o.Author)
                .WithMany(o => o.CreatedIssues)
                .HasForeignKey(o => o.AuthorId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasOne(o => o.UserWhoClosed)
                .WithMany(o => o.ClosedIssues)
                .HasForeignKey(o => o.UserIdWhoClosed)
                .HasPrincipalKey(o => o.UserId);

            builder.HasMany(o => o.TimeEntries)
                .WithOne(o => o.Issue)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
            
            builder.HasMany(o => o.Comments)
                .WithOne(o => o.Issue)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);

            builder.HasMany(o => o.IssueAssignees)
                .WithOne(o => o.Issue)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
            
            builder.HasMany(o => o.IssueLabels)
                .WithOne(o => o.Issue)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
            
            builder.HasMany(o => o.IssueUserReactions)
                .WithOne(o => o.Issue)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
        });

        modelBuilder.Entity<Reaction>(builder =>
        {
            builder.ToTable("Reaction");
            builder.HasKey(r => r.ReactionId);
            builder.Property(r => r.ReactionId)
                .ValueGeneratedNever();
            builder.Property(r => r.Unicode)
                .IsRequired();
            builder.Property(r => r.Description)
                .IsRequired();
            
            builder.HasMany(o => o.IssueUserReactions)
                .WithOne(o => o.Reaction)
                .HasForeignKey(o => o.ReactionId)
                .HasPrincipalKey(o => o.ReactionId);
            
            builder.HasMany(o => o.CommentReactions)
                .WithOne(o => o.Reaction)
                .HasForeignKey(o => o.CommentId)
                .HasPrincipalKey(o => o.ReactionId);
        });

        modelBuilder.Entity<TimeEntry>(builder =>
        {
            builder.ToTable("TimeEntry");
            builder.HasKey(o => o.TimeEntryId);
            builder.Property(o => o.TimeEntryId)
                .ValueGeneratedNever();
            builder.Property(o => o.Hours)
                .IsRequired();
            builder.Property(o => o.Description)
                .IsRequired(false);
            builder.Property(o => o.DueDate)
                .IsRequired(false);
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property(o => o.ConcurrencyToken)
                .IsConcurrencyToken();
            
            builder.HasOne(o => o.Issue)
                .WithMany(o => o.TimeEntries)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);

            builder.HasOne(o => o.User)
                .WithMany(o => o.TimeEntries)
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(o => o.UserId);
        });

        modelBuilder.Entity<Label>(builder =>
        {
            builder.ToTable("Label");
            builder.HasKey(o => o.LabelId);
            builder.Property(o => o.LabelId)
                .ValueGeneratedNever();
            builder.Property(o => o.Title)
                .IsRequired();
            builder.Property(o => o.Description)
                .IsRequired(false);
            builder.Property(o => o.BackgroundColor)
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property(o => o.ConcurrencyToken)
                .IsConcurrencyToken();
            
            builder.HasMany(o => o.IssueLabels)
                .WithOne(o => o.Label)
                .HasForeignKey(o => o.LabelId)
                .HasPrincipalKey(o => o.LabelId);
        });
        
        modelBuilder.Entity<Comment>(builder =>
        {
            builder.ToTable("Comment");
            builder.HasKey(o => o.CommentId);
            builder.Property(o => o.CommentId)
                .ValueGeneratedNever();
            builder.Property(o => o.Content)
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property(o => o.ConcurrencyToken)
                .IsConcurrencyToken();
            
            builder.HasOne(o => o.Author)
                .WithMany(o => o.Comments)
                .HasForeignKey(o => o.AuthorId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasOne(o => o.Issue)
                .WithMany(o => o.Comments)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);

            builder.HasOne(o => o.ParentComment)
                .WithMany(o => o.ChildComments)
                .HasForeignKey(o => o.ParentCommentId)
                .HasPrincipalKey(o => o.CommentId);
            // .OnDelete(DeleteBehavior.Cascade); // optional
            
            builder.HasMany(o => o.CommentUserReactions)
                .WithOne(o => o.Comment)
                .HasForeignKey(o => o.CommentId)
                .HasPrincipalKey(o => o.CommentId);
        });
        
        modelBuilder.Entity<CommentUserReaction>(builder =>
        {
            builder.ToTable("CommentUserReaction");
            builder.HasKey(o => o.CommentUserReactionId);

            builder.HasOne(o => o.Comment)
                .WithMany(o => o.CommentUserReactions)
                .HasForeignKey(o => o.CommentId)
                .HasPrincipalKey(o => o.CommentId);

            builder.HasOne(o => o.User)
                .WithMany(o => o.CommentUserReactions)
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(o => o.UserId);
            
            builder.HasOne(o => o.Reaction)
                .WithMany(o => o.CommentReactions)
                .HasForeignKey(o => o.CommentId)
                .HasPrincipalKey(o => o.ReactionId);
        });
        
        modelBuilder.Entity<IssueAssignee>(builder =>
        {
            builder.ToTable("IssueAssignee");
            builder.HasKey(o => new { o.IssueId, o.AssigneeId });

            builder.HasOne(o => o.Issue)
                .WithMany(o => o.IssueAssignees)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
            
            builder.HasOne(o => o.Assignee)
                .WithMany(o => o.IssueAssignees)
                .HasForeignKey(o => o.AssigneeId)
                .HasPrincipalKey(o => o.UserId);
        });
        
        modelBuilder.Entity<IssueLabel>(builder =>
        {
            builder.ToTable("IssueLabel");
            builder.HasKey(o => new { o.IssueId, o.LabelId });

            builder.HasOne(o => o.Issue)
                .WithMany(o => o.IssueLabels)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
            
            builder.HasOne(o => o.Label)
                .WithMany(o => o.IssueLabels)
                .HasForeignKey(o => o.LabelId)
                .HasPrincipalKey(o => o.LabelId);
        });
        
        modelBuilder.Entity<IssueUserReaction>(builder =>
        {
            builder.ToTable("IssueUserReaction");
            builder.HasKey(o => o.IssueUserReactionId);

            builder.HasOne(o => o.Issue)
                .WithMany(o => o.IssueUserReactions)
                .HasForeignKey(o => o.IssueId)
                .HasPrincipalKey(o => o.IssueId);
            
            builder.HasOne(o => o.User)
                .WithMany(o => o.IssueUserReactions)
                .HasForeignKey(o => o.UserId)
                .HasPrincipalKey(o => o.UserId);
            
            builder.HasOne(o => o.Reaction)
                .WithMany(o => o.IssueUserReactions)
                .HasForeignKey(o => o.ReactionId)
                .HasPrincipalKey(o => o.ReactionId);
        });
    }
}