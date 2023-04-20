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
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id)
                .HasColumnName("UserId")
                .ValueGeneratedNever();
            builder.Property(o => o.Name)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(o => o.Email)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(o => o.PasswordHash)
                .HasMaxLength(1024)
                .IsRequired();
            builder.Property(o => o.Role)
                .HasMaxLength(64)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(o => o.State)
                .HasMaxLength(64)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(o => o.IsDeleted)
                .IsRequired();
            builder.Property(o => o.CreateDate)
                .IsRequired();
            builder.Property(o => o.UpdateDate)
                .IsRequired();
            builder.Property("_concurrencyToken")
                .HasColumnName("ConcurrencyToken")
                .IsConcurrencyToken();
        });
    }
}