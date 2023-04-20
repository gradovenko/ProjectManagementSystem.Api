using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Reactions;

public sealed class ReactionQueryDbContext : DbContext
{
    public ReactionQueryDbContext(DbContextOptions<ReactionQueryDbContext> options) : base(options) { }

    internal DbSet<Reaction> Reactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reaction>(builder =>
        {
            builder.ToTable("Reaction");
            builder.HasKey(o => o.ReactionId);
            builder.Property(o => o.Emoji)
                .IsRequired();
            builder.Property(o => o.Name)
                .IsRequired();
            builder.Property(o => o.Category)
                .IsRequired();

            // builder.HasMany(o => o.IssueUserReactions)
            //     .WithOne(o => o.Reaction)
            //     .HasForeignKey(o => o.ReactionId)
            //     .HasPrincipalKey(o => o.ReactionId);
            //
            // builder.HasMany(o => o.CommentReactions)
            //     .WithOne(o => o.Reaction)
            //     .HasForeignKey(o => o.CommentId)
            //     .HasPrincipalKey(o => o.ReactionId);
        });
    }
}