using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.IssueStatuses;

namespace ProjectManagementSystem.Infrastructure.Admin.IssueStatuses
{
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
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(@is => @is.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(@is => @is.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();
            });
        }
    }
}