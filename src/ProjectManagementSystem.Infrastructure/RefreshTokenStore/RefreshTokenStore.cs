using Microsoft.EntityFrameworkCore;

namespace ProjectManagementSystem.Infrastructure.RefreshTokenStore;

public sealed class RefreshTokenStore : Domain.Authentication.IRefreshTokenStore, Domain.Users.IRefreshTokenStore
{
    private readonly TimeSpan _lifeTime = TimeSpan.FromDays(1);
    private readonly RefreshTokenStoreDbContext _context;

    public RefreshTokenStore(RefreshTokenStoreDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<string> Add(Guid userId, CancellationToken cancellationToken)
    {
        RefreshToken refreshToken = new RefreshToken(Guid.NewGuid().ToString(), userId, _lifeTime);

        _context.RefreshTokens.Add(refreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return refreshToken.Id;
    }

    public async Task<(string Value, Guid UserId)?> Reissue(string refreshToken, CancellationToken cancellationToken)
    {
        RefreshToken? oldRefreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(token =>
            token.Id == refreshToken
            && token.ExpireDate > DateTime.UtcNow, cancellationToken: cancellationToken);

        if (oldRefreshToken == null)
            return null;

        oldRefreshToken.Terminate();

        RefreshToken newRefreshToken = new RefreshToken(Guid.NewGuid().ToString(), oldRefreshToken.UserId, _lifeTime);

        _context.RefreshTokens.Add(newRefreshToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new ValueTuple<string, Guid>(newRefreshToken.Id, newRefreshToken.UserId);
    }

    public async Task ExpireAllTokens(Guid userId, CancellationToken cancellationToken)
    {
        var activeRefreshTokens = await _context.RefreshTokens
            .Where(rt => rt.UserId == userId)
            .Where(rt => rt.ExpireDate > DateTime.UtcNow)
            .ToListAsync(cancellationToken);

        foreach (var activeRefreshToken in activeRefreshTokens)
            activeRefreshToken.Terminate();

        await _context.SaveChangesAsync(cancellationToken);
    }
}