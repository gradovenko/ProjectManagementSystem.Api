using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Users;

namespace ProjectManagementSystem.Infrastructure.Users;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(builder =>
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
                .HasMaxLength(1024);
            builder.Property(u => u.Role)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(u => u.CreateDate)
                .IsRequired();
            builder.Property(u => u.UpdateDate)
                .IsRequired();
            builder.Property(u => u.State)
                .HasConversion<string>()
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
        });
    }
}