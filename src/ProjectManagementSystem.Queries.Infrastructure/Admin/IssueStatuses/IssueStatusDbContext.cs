using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses;

public sealed class IssueStatusDbContext : DbContext
{
    public IssueStatusDbContext(DbContextOptions<IssueStatusDbContext> options) : base(options) { }
        
    internal DbSet<IssueStatus> IssueStatuses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IssueStatus>(builder =>
        {
            builder.ToTable("IssueStatus");
            builder.HasKey(@is => @is.Id);
            builder.Property(@is => @is.Id)
                .HasColumnName("IssueStatusId");
            builder.Property(@is => @is.Name);
            builder.Property(@is => @is.IsActive);
        });
    }
}