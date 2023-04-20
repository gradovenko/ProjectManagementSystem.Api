using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueReactions;

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

            builder.HasMany(o => o.Issues)
                .WithMany(o => o.Reactions)
                .UsingEntity<IssueUserReaction>(
                    r => r.HasOne<Issue>().WithMany().HasForeignKey(e =>  e.IssueId),
                    l => l.HasOne<Reaction>().WithMany().HasForeignKey(e => e.ReactionId));
        });

        modelBuilder.Entity<IssueUserReaction>(builder =>
        {
            builder.ToTable("IssueUserReaction");
            builder.HasKey(o => new { o.IssueId, o.ReactionId });
        });

        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(o => o.IssueId);
        });
    }
}