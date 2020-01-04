using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.CreateTrackers;

namespace ProjectManagementSystem.Infrastructure.Admin.CreateTrackers
{
    public sealed class TrackerDbContext : DbContext
    {
        public TrackerDbContext(DbContextOptions<TrackerDbContext> options) : base(options) { }
        
        internal DbSet<Tracker> Trackers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tracker>(builder =>
            {
                builder.ToTable("Tracker");
                builder.HasKey(t => t.Id);
                builder.Property(t => t.Id)
                    .HasColumnName("TrackerId")
                    .ValueGeneratedNever();
                builder.Property(t => t.Name)
                    .HasColumnName("Name");
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });
        }
    }
}