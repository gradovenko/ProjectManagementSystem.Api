using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class CommentDbContext : DbContext
{
    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

    internal DbSet<Issue> Issues { get; init; }
    internal DbSet<User> Users { get; init; }
    internal DbSet<Comment> Comments { get; set; }
    internal DbSet<Reaction> Reactions { get; set; }
    internal DbSet<CommentUserReaction> CommentUserReactions { get; set; }    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
                
        modelBuilder.Entity<Comment>(builder =>
        {
            builder.ToTable("Comment");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("CommentId")
                .ValueGeneratedNever();
            builder.Property(o => o.Content)
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();

            builder.HasOne(o => o.ParentComment)
                .WithMany(o => o.ChildComments)
                .HasForeignKey(o => o.ParentCommentId)
                .HasPrincipalKey(o => o.Id);
        });

        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("IssueId")
                .ValueGeneratedNever();
        });
        
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("UserId")
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

        
        modelBuilder.Entity<CommentUserReaction>(builder =>
        {
            builder.ToTable("CommentUserReaction");
            builder.HasKey(o => new {o.CommentId, o.UserId, o.ReactionId});

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