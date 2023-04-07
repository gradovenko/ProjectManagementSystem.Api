using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Labels;

public sealed class LabelQueryDbContext : DbContext
{
    public LabelQueryDbContext(DbContextOptions<LabelQueryDbContext> options) : base(options) { }

    internal DbSet<Label> Labels { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Label>(builder =>
        {
            builder.ToTable("Label");
            builder.HasKey(l => l.LabelId);
            builder.Property(l => l.Title)
                .IsRequired();
            builder.Property(l => l.Description)
                .IsRequired(false);
            builder.Property(l => l.BackgroundColor)
                .IsRequired();
            builder.Property(l => l.IsDeleted)
                .IsRequired();
            builder.Property(l => l.CreateDate)
                .IsRequired();
            builder.Property(l => l.UpdateDate)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
        });
    }
}
