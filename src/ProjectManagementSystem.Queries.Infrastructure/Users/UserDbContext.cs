using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Queries.Infrastructure.Users;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(builder =>
        {
            builder.ToTable("User");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId)
                .HasColumnName("UserId");
            builder.Property(u => u.Name);
            builder.Property(u => u.Email);
        });
    }
}