using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.TimeEntries;

namespace ProjectManagementSystem.Infrastructure.TimeEntries;

public sealed class TimeEntryDbContext : DbContext
{
    public TimeEntryDbContext(DbContextOptions<TimeEntryDbContext> options) : base(options) { }

    internal DbSet<Issue> Issues { get; init; }
    internal DbSet<User> Users { get; init; }
    internal DbSet<TimeEntry> TimeEntries { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TimeEntry>(builder =>
        {
            builder.ToTable("TimeEntry");
            builder.HasKey(te => te.Id);
            builder.Property(te => te.Id)
                .HasColumnName("TimeEntryId")
                .ValueGeneratedNever();
            builder.Property(te => te.Hours)
                .IsRequired();
            builder.Property(te => te.Description)
                .IsRequired(false);
            builder.Property(te => te.DueDate)
                .IsRequired(false);
            builder.Property(te => te.CreateDate)
                .IsRequired();
            builder.Property(te => te.IssueId)
                .IsRequired();
            builder.Property(te => te.UserId)
                .IsRequired();
            builder.Property(te => te.IsDeleted)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
        });

        modelBuilder.Entity<Issue>(builder =>
        {
            builder.ToTable("Issue");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
                .HasColumnName("IssueId")
                .ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
                .HasColumnName("UserId")
                .ValueGeneratedNever();
        });
    }
}