using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.User.Accounts
{
    public sealed class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        internal DbSet<Domain.User.Accounts.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Domain.User.Accounts.User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id)
                    .HasColumnName("UserId")
                    .ValueGeneratedNever();
                builder.Property(u => u.Name)
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.Email)
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.PasswordHash)
                    .HasMaxLength(1024)
                    .IsRequired();
                builder.Property(u => u.UpdateDate);
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });
        }
    }
}