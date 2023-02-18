using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.IssuePriorities;

namespace ProjectManagementSystem.Infrastructure.Admin.IssuePriorities;

public sealed class IssuePriorityDbContext : DbContext
{
    public IssuePriorityDbContext(DbContextOptions<IssuePriorityDbContext> options) : base(options) { }
        
    internal DbSet<IssuePriority> IssuePriorities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IssuePriority>(builder =>
        {
            builder.ToTable("IssuePriority");
            builder.HasKey(ip => ip.Id);
            builder.Property(ip => ip.Id)
                .HasColumnName("IssuePriorityId")
                .ValueGeneratedNever();
            builder.Property(ip => ip.Name)
                .IsRequired();
            builder.Property(ip => ip.IsActive)
                .IsRequired();
        });
    }
}