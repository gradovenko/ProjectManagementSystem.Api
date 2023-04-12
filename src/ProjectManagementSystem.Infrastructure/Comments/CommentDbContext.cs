using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class CommentDbContext : DbContext
{
    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

    internal DbSet<Issue> Issues { get; init; }
    internal DbSet<User> Users { get; init; }
    internal DbSet<Comment> Comments { get; set; }
    
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

            // builder.HasOne(o => o.Author)
            //     .WithMany(o => o.Comments)
            //     .HasForeignKey(o => o.AuthorId)
            //     .HasPrincipalKey(o => o.UserId);
            //
            // builder.HasOne(o => o.Issue)
            //     .WithMany(o => o.Comments)
            //     .HasForeignKey(o => o.IssueId)
            //     .HasPrincipalKey(o => o.IssueId);

            builder.HasOne(o => o.ParentComment)
                .WithMany(o => o.ChildComments)
                .HasForeignKey(o => o.ParentCommentId)
                .HasPrincipalKey(o => o.Id);
            // .OnDelete(DeleteBehavior.Cascade); // optional
            
            // builder.HasMany(o => o.CommentReactions)
            //     .WithOne(o => o.Comment)
            //     .HasForeignKey(o => o.CommentId)
            //     .HasPrincipalKey(o => o.CommentId);
        });
    }
}