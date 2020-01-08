using System;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Projects
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
                    .HasColumnName("ProjectId");
                builder.Property(p => p.Name);
                builder.Property(p => p.Description);
                builder.Property(p => p.IsPrivate);
                builder.Property(p => p.Status)
                    .HasConversion(
                        ps => ps.ToString(),
                        ps => (ProjectStatus) Enum.Parse(typeof(ProjectStatus), ps));
                builder.Property(p => p.CreateDate);
            });
        }
    }
}