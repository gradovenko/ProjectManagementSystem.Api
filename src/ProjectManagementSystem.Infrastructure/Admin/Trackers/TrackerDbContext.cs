using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.Trackers;

namespace ProjectManagementSystem.Infrastructure.Admin.Trackers
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
                builder.Property(t => t.Name);
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });
        }
    }
}