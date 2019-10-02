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
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(u => u.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.Email)
                    .HasColumnName("Email")
                    .HasMaxLength(256)
                    .IsRequired();
                builder.Property(u => u.PasswordHash)
                    .HasColumnName("PasswordHash")
                    .HasMaxLength(1024)
                    .IsRequired();
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

                builder.HasIndex(u => u.Name)
                    .HasName("UserNameIndex")
                    .IsUnique();
                builder.HasIndex(u => u.Email)
                    .HasName("EmailIndex")
                    .IsUnique();
                
                builder.HasData(new User
                {
                    Id = new Guid("0ae12bbd-58ef-4c2e-87a6-2c2cb3f9592d"),
                    Name = "Admin",
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
                builder.HasKey(rt => rt.Id);

                builder.Property(rt => rt.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(rt => rt.ExpireDate)
                    .HasColumnName("ExpireDate")
                    .IsRequired();
                builder.Property(rt => rt.UserId)
                    .HasColumnName("UserId")
                    .IsRequired();
            });
            
            modelBuilder.Entity<IssuePriority>(builder =>
            {
                builder.ToTable("IssuePriority");
                builder.HasKey(ip => ip.Id);

                builder.Property(ip => ip.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(ip => ip.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(ip => ip.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();
            });
            
            modelBuilder.Entity<IssueStatus>(builder =>
            {
                builder.ToTable("IssueStatus");
                builder.HasKey(@is => @is.Id);

                builder.Property(@is => @is.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(@is => @is.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(@is => @is.IsActive)
                    .HasColumnName("IsActive")
                    .IsRequired();
            });
            
            modelBuilder.Entity<Project>(builder =>
            {
                builder.ToTable("Project");
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(p => p.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(p => p.Description)
                    .HasColumnName("Description")
                    .IsRequired();
                builder.Property(p => p.IsPublic)
                    .HasColumnName("IsPublic")
                    .IsRequired();
                builder.Property(p => p.Status)
                    .HasColumnName("Status")
                    .HasConversion(
                        ps => ps.ToString(),
                        ps => (ProjectStatus) Enum.Parse(typeof(ProjectStatus), ps))
                    .IsRequired();
                builder.Property(p => p.CreateDate)
                    .HasColumnName("CreateDate")
                    .IsRequired();
                builder.Property(u => u.ConcurrencyStamp)
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });
            
            modelBuilder.Entity<Tracker>(builder =>
            {
                builder.ToTable("Tracker");
                builder.HasKey(t => t.Id);

                builder.Property(t => t.Id)
                    .HasColumnName("Id")
                    .ValueGeneratedNever()
                    .IsRequired();
                builder.Property(t => t.Name)
                    .HasColumnName("Name")
                    .IsRequired();
                builder.Property(t => t.ConcurrencyStamp)
                    .HasColumnName("ConcurrencyStamp")
                    .IsConcurrencyToken();
            });
        }
    }
}