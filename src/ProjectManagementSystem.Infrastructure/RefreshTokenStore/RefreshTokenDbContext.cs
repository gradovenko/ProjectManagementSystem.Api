using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore
{
    public sealed class RefreshTokenDbContext : DbContext
    {
        public RefreshTokenDbContext(DbContextOptions<RefreshTokenDbContext> options) : base(options) { }

        internal DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RefreshToken>(builder =>
            {
                builder.ToTable("RefreshToken");
                builder.HasKey(ut => ut.Id);

                builder.Property(u => u.Id)
                    .ValueGeneratedNever();
                builder.Property(ut => ut.ExpireDate)
                    .IsRequired();
                builder.Property(ut => ut.UserId)
                    .IsRequired();
            });
        }
    }
}