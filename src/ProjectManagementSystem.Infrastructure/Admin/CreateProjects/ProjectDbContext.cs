using System;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.CreateProjects;

namespace ProjectManagementSystem.Infrastructure.Admin.CreateProjects
{
    public sealed class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
        
        internal DbSet<Project> Projects { get; set; }
        internal DbSet<Tracker> Trackers { get; set; }

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
            
            modelBuilder.Entity<ProjectTracker>(builder =>
            {
                builder.ToTable("ProjectTracker");
                builder.HasKey(pt => new { pt.ProjectId, pt.TrackerId });
            });
        }
    }
}