using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.IssueTimeEntries;

public sealed class TimeEntryQueryDbContext : DbContext
{
    public TimeEntryQueryDbContext(DbContextOptions<TimeEntryQueryDbContext> options) : base(options) { }

    internal DbSet<TimeEntry> TimeEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<TimeEntry>(builder =>
        {
            builder.ToTable("TimeEntry");
            builder.HasKey(o => o.TimeEntryId);
            builder.Property(o => o.Hours)
                .IsRequired();
            builder.Property(o => o.Description)
                .IsRequired(false);
            builder.Property(o => o.DueDate)
                .IsRequired(false);
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            
            builder.HasOne(o => o.Author)
                .WithMany()
                .HasForeignKey(o => o.AuthorId)
                .HasPrincipalKey(o => o.UserId);
        });
        
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(o => o.UserId);
        });
    }
}