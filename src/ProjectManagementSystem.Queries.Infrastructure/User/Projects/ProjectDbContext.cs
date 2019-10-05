using System;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Projects
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
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(p => p.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(p => p.Description)
                    .HasColumnName("Description")
                    .IsRequired();
                builder.Property(p => p.IsPrivate)
                    .HasColumnName("IsPrivate")
                    .IsRequired();
                builder.Property(p => p.Status)
                    .HasColumnName("Status")
                    .HasConversion(
                        ps => ps.ToString(),
                        ps => (ProjectStatus) Enum.Parse(typeof(ProjectStatus), ps))
                    .IsRequired();
                builder.Property(p => p.CreateDate)
                    .HasColumnName("CreateDate")
                    .IsRequired();
                builder.Property(p => p.UpdateDate)
                    .HasColumnName("UpdateDate");
            });
        }
    }
}