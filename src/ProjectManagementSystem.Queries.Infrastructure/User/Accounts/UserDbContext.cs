using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Accounts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options) { }

        internal DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);

                builder.Property(u => u.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
                builder.Property(u => u.UserName)
                    .HasColumnName("UserName")
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(256)
                    .IsRequired();
            });
        }
    }
}