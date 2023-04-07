using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class CommentDbContext : DbContext
{
    public CommentDbContext(DbContextOptions<CommentDbContext> options) : base(options) { }

    internal DbSet<Domain.Comments.Comment> Comments { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Comments.Comment>(builder =>
        {
            builder.ToTable("Comment");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Id)
                .HasColumnName("CommentId")
                .ValueGeneratedNever();
        });
    }
}