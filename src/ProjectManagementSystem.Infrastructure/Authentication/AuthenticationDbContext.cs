using System;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Authentication;

namespace ProjectManagementSystem.Infrastructure.Authentication
{
    public sealed class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options) { }

        internal DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);

                builder.Property(u => u.Id)
                    .ValueGeneratedNever();
                builder.Property(u => u.UserName)
                    .HasColumnName("UserName")
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.PasswordHash)
                    .HasColumnName("PasswordHash")
                    .HasMaxLength(1024);
                builder.Property(u => u.Role)
                    .HasColumnName("Role")
                    .HasConversion(
                        r => r.ToString(),
                        r => (UserRole) Enum.Parse(typeof(UserRole), r))
                    .IsRequired();

                builder.HasIndex(u => u.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();
                
                builder.HasIndex(u => u.Email)
                    .HasName("EmailIndex")
                    .IsUnique();
            });
        }
    }
}