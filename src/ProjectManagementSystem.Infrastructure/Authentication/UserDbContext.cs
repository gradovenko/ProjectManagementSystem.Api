using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.Authentication;

public sealed class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

    internal DbSet<Domain.Authentication.User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Authentication.User>(builder =>
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
        });
    }
}