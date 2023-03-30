using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Labels;

namespace ProjectManagementSystem.Infrastructure.Labels;

public sealed class LabelDbContext : DbContext
{
    public LabelDbContext(DbContextOptions<LabelDbContext> options) : base(options) { }

    internal DbSet<Project> Projects { get; init; }
    internal DbSet<Label> Labels { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Label>(builder =>
        {
            builder.ToTable("Label");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Id)
                .HasColumnName("LabelId")
                .ValueGeneratedNever();
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

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ProjectId")
                .ValueGeneratedNever();
        });
    }
}
