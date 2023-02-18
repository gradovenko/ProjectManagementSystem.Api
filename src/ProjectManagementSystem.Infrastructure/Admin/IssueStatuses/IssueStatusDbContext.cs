using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.IssueStatuses;

namespace ProjectManagementSystem.Infrastructure.Admin.IssueStatuses;

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
                .HasColumnName("IssueStatusId")
                .ValueGeneratedNever();
            builder.Property(@is => @is.Name)
                .IsRequired();
            builder.Property(@is => @is.IsActive)
                .IsRequired();
        });
    }
}