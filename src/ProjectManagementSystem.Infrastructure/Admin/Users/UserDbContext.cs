using System;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Admin.CreateUsers;

namespace ProjectManagementSystem.Infrastructure.Admin.Users
{
    public sealed class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        internal DbSet<Domain.Admin.CreateUsers.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Admin.CreateUsers.User>(builder =>
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
                builder.Property(u => u.FirstName)
                    .IsRequired();
                builder.Property(u => u.LastName)
                    .IsRequired();
                builder.Property(u => u.Role)
                    .HasConversion<string>()
                    .IsRequired();
                builder.Property(u => u.CreateDate)
                    .IsRequired();
                builder.Property(u => u.Status)
                    .HasConversion<string>()
                    .IsRequired();
                builder.Property("_concurrencyStamp")
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });
        }
    }
}