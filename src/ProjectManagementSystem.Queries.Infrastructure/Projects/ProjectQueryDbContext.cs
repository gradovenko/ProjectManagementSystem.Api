using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Projects;

public sealed class ProjectQueryDbContext : DbContext
{
    public ProjectQueryDbContext(DbContextOptions<ProjectQueryDbContext> options) : base(options) { }

    internal DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Project>(builder =>
        {
            builder.ToTable("Project");
            builder.HasKey(p => p.ProjectId);
            builder.Property(p => p.ProjectId)
                .ValueGeneratedNever();
            builder.Property(p => p.Name)
                .IsRequired();
            builder.Property(p => p.Description)
                .IsRequired();
            builder.Property(p => p.Path)
                .IsRequired();
            builder.Property(p => p.Visibility)
                .HasMaxLength(64)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired();
            builder.Property(p => p.UpdateDate)
                .IsRequired();
        });
    }
}