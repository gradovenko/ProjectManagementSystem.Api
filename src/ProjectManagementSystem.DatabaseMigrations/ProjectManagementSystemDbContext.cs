using System;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.DatabaseMigrations.Entities;

namespace ProjectManagementSystem.DatabaseMigrations
{
    public sealed class ProjectManagementSystemDbContext : DbContext
    { 
        public ProjectManagementSystemDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                builder.Property(u => u.PasswordHash)
                    .HasColumnName("PasswordHash")
                    .HasMaxLength(1024);
                builder.Property(u => u.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                builder.Property(u => u.LastName)
                    .HasColumnName("LastName")
                    .IsRequired();
                builder.Property(u => u.Role)
                    .HasColumnName("Role")
                    .HasConversion(
                        r => r.ToString(),
                        r => (UserRole) Enum.Parse(typeof(UserRole), r))
                    .IsRequired();
                builder.Property(u => u.CreateDate)
                    .HasColumnName("CreateDate")
                    .IsRequired();
                builder.Property(u => u.UpdateDate)
                    .HasColumnName("UpdateDate");
                builder.Property(u => u.Status)
                    .HasColumnName("Status")
                    .HasConversion(
                        s => s.ToString(),
                        s => (UserStatus) Enum.Parse(typeof(UserStatus), s))
                    .IsRequired();
                builder.Property(u => u.ConcurrencyStamp)
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();

                builder.HasIndex(u => u.UserName)
                    .HasName("UserNameIndex")
                    .IsUnique();
                builder.HasIndex(u => u.Email)
                    .HasName("EmailIndex")
                    .IsUnique();
                
                builder.HasData(new User
                {
                    Id = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                    UserName = "Admin",
                    Email = "admin@projectms.local",
                    PasswordHash = "AQAAAAEAACcQAAAAEDcxbCGbTbY1rUJBVafqc/qaL1rWXro6aoahEwrPF5zHb8DB11apWESUm5UyMRF3mA==",
                    FirstName = "Admin",
                    LastName = "Admin",
                    Role = UserRole.Admin,
                    CreateDate = DateTime.UnixEpoch,
                    Status = UserStatus.Active,
                    ConcurrencyStamp = Guid.NewGuid()
                });

            });
            
            modelBuilder.Entity<RefreshToken>(builder =>
            {
                builder.ToTable("RefreshToken");
                builder.HasKey(ut => ut.Id);

                builder.Property(u => u.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever();
                builder.Property(ut => ut.ExpireDate)
                    .HasColumnName("ExpireDate")
                    .IsRequired();
                builder.Property(ut => ut.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();
            });
        }
    }
}