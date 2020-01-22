using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public sealed class IssuePriorityDbContext : DbContext
    {
        public IssuePriorityDbContext(DbContextOptions<IssuePriorityDbContext> options) : base(options) { }
        
        internal DbSet<IssuePriority> IssuePriorities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IssuePriority>(builder =>
            {
                builder.ToTable("IssuePriority");
                builder.HasKey(ip => ip.Id);
                builder.Property(ip => ip.Id)
                    .HasColumnName("IssuePriorityId");
                builder.Property(ip => ip.Name);
                builder.Property(ip => ip.IsActive);
            });
        }
    }
}