using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.User.Members
{
    public sealed class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options) : base(options) { }

        internal DbSet<Domain.User.Members.User> Users { get; set; }
        internal DbSet<Domain.User.Members.Project> Projects { get; set; }
        internal DbSet<Domain.User.Members.Permission> Permissions { get; set; }
        internal DbSet<Domain.User.Members.Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.User.Members.User>(builder =>
            {
                builder.ToTable("User");
                builder.HasKey(u => u.Id);
                builder.Property(u => u.Id)
                    .HasColumnName("UserId");
            });

            modelBuilder.Entity<Domain.User.Members.Project>(builder =>
            {
                builder.ToTable("Project");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .HasColumnName("ProjectId");
            });

            modelBuilder.Entity<Domain.User.Members.RolePermission>(builder =>
            {
                builder.ToTable("RolePermission");
                builder.HasKey(rp => new {rp.RoleId, rp.PermissionId});
            });

            modelBuilder.Entity<Domain.User.Members.Permission>(builder =>
            {
                builder.ToTable("Permission");
                builder.HasKey(p => p.Id);
                builder.Property(p => p.Id)
                    .HasColumnName("PermissionId");
                builder.HasMany(p => p.RolePermissions)
                    .WithOne()
                    .HasForeignKey(rp => rp.PermissionId)
                    .HasPrincipalKey(p => p.Id);
            });

            modelBuilder.Entity<Domain.User.Members.Member>(builder =>
            {
                builder.ToTable("Member");
                builder.HasOne(m => m.User)
                    .WithMany()
                    .HasForeignKey(m => m.UserId)
                    .HasPrincipalKey(u => u.Id);
                builder.HasOne(m => m.Project)
                    .WithMany()
                    .HasForeignKey(m => m.ProjectId)
                    .HasPrincipalKey(p => p.Id);
                builder.HasOne(m => m.Role)
                    .WithMany()
                    .HasForeignKey(m => m.RoleId)
                    .HasPrincipalKey(r => r.Id);
            });
        }
    }
}