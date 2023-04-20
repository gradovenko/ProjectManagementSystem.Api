using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueComments;

public sealed class CommentQueryDbContext : DbContext
{
    public CommentQueryDbContext(DbContextOptions<CommentQueryDbContext> options) : base(options) { }

    internal DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
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

            builder.HasOne(o => o.Author)
                .WithMany()
                .HasForeignKey(o => o.AuthorId)
                .HasPrincipalKey(o => o.UserId);

            builder.HasOne(o => o.ParentComment)
                .WithMany(o => o.ChildComments)
                .HasForeignKey(o => o.ParentCommentId)
                .HasPrincipalKey(o => o.CommentId);
        });
 
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(o => o.UserId);
        });
    }
}