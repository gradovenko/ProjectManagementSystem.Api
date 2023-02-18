using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.TimeEntryActivities;

namespace ProjectManagementSystem.Infrastructure.Admin.TimeEntryActivities;

public sealed class TimeEntryActivityDbContext : DbContext
{
    public TimeEntryActivityDbContext(DbContextOptions<TimeEntryActivityDbContext> options) : base(options) { }
        
    internal DbSet<TimeEntryActivity> TimeEntryActivities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TimeEntryActivity>(builder =>
        {
            builder.ToTable("TimeEntryActivity");
            builder.HasKey(tea => tea.Id);
            builder.Property(tea => tea.Id)
                .HasColumnName("TimeEntryActivityId")
                .IsRequired();
            builder.Property(tea => tea.Name)
                .IsRequired();
            builder.Property(tea => tea.IsActive)
                .IsRequired();
            builder.Property("_concurrencyStamp")
                .HasColumnName("ConcurrencyStamp")
                .IsConcurrencyToken();
        });
    }
}