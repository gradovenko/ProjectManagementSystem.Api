using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.CreateProjectTrackers;

namespace ProjectManagementSystem.Infrastructure.Admin.CreateProjectTrackers
{
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
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
                
                builder.HasMany(p => p.ProjectTrackers)
                    .WithOne()
                    .HasForeignKey(pt => pt.ProjectId)
                    .HasPrincipalKey(p => p.Id);
            });
            
            modelBuilder.Entity<Tracker>(builder =>
            {
                builder.ToTable("Tracker");
                builder.HasKey(t => t.Id);

                builder.Property(t => t.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
            });
        }
    }
}