using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Reactions;

namespace ProjectManagementSystem.Infrastructure.Reactions;

public sealed class ReactionDbContext : DbContext
{
    public ReactionDbContext(DbContextOptions<ReactionDbContext> options) : base(options) { }

    internal DbSet<Reaction> Reactions { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reaction>(builder =>
        {
            builder.ToTable("Reaction");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("ReactionId")
                .ValueGeneratedNever();
            builder.Property(o => o.Emoji)
                .IsRequired();
            builder.Property(o => o.Name)
                .IsRequired();
            builder.Property(o => o.Category)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
        });
    }
}