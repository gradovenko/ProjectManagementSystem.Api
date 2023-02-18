using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Authentication;

namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore;

public sealed class RefreshTokenStore : IRefreshTokenStore
{
    private readonly RefreshTokenDbContext _context;

    public RefreshTokenStore(RefreshTokenDbContext context)
    {
        _context = context;
    }
        
    public async Task<string> Create(Guid userId)
    {
        var refreshToken = new RefreshToken(Guid.NewGuid().ToString(), TimeSpan.FromDays(1), userId);

        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync();

        return refreshToken.Id;
    }

    public async Task<Domain.Authentication.RefreshToken> Reissue(string refreshToken)
    {
        var oldRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(rt => rt.Id == refreshToken && rt.ExpireDate > DateTime.UtcNow);

        if (oldRefreshToken == null)
            return null;

        oldRefreshToken.Terminate();

        var newRefreshToken = new RefreshToken(Guid.NewGuid().ToString(), TimeSpan.FromDays(1), oldRefreshToken.UserId);

        _context.RefreshTokens.Add(newRefreshToken);

        await _context.SaveChangesAsync();

        return new Domain.Authentication.RefreshToken(newRefreshToken.Id, newRefreshToken.UserId);
    }
}