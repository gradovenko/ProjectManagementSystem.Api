using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Projects;

namespace ProjectManagementSystem.Infrastructure.Projects;

public sealed class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }

    internal DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .HasColumnName("ProjectId")
                .ValueGeneratedNever();
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired(false);
            builder.Property(p => p.Path)
                .IsRequired();
            builder.Property(p => p.Visibility)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired();
            builder.Property(p => p.UpdateDate)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
        });
    }
}