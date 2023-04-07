using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore;

public sealed class RefreshTokenStoreDbContext : DbContext
{
    public RefreshTokenStoreDbContext(DbContextOptions<RefreshTokenStoreDbContext> options) : base(options) { }

    internal DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RefreshToken>(builder =>
        {
            builder.ToTable("RefreshToken");
            builder.HasKey(refreshToken => refreshToken.Id);
            builder.Property(refreshToken => refreshToken.Id)
                .HasColumnName("RefreshTokenId")
                .HasMaxLength(64);
            builder.Property(refreshToken => refreshToken.UserId)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(refreshToken => refreshToken.ExpireDate)
                .IsRequired();
        });
    }
}