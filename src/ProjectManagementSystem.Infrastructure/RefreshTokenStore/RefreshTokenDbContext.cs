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
                builder.HasKey(rt => rt.Id);
                builder.Property(rt => rt.Id)
                    .HasColumnName("RefreshTokenId")
                    .ValueGeneratedNever();
                builder.Property(rt => rt.ExpireDate)
                    .IsRequired();
                builder.Property(rt => rt.UserId)
                    .IsRequired();
            });
        }
    }
}