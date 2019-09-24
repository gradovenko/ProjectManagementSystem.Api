using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public class IssuePriorityDbContext : DbContext
    {
        public IssuePriorityDbContext(DbContextOptions options) : base(options) { }
        
        internal DbSet<IssuePriority> IssuePriorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IssuePriority>(builder =>
            {
                builder.ToTable("IssuePriority");
                builder.HasKey(ip => ip.Id);

                builder.Property(ip => ip.Id)
                    .HasColumnName("Id")
                    .IsRequired();
                builder.Property(ip => ip.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(ip => ip.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();
            });
        }
    }
}