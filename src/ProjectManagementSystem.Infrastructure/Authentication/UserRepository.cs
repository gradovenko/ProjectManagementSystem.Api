using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Authentication;

namespace ProjectManagementSystem.Infrastructure.Authentication;

public sealed class UserRepository : IUserGetter
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User> Get(Guid userId, CancellationToken cancellationToken)
    {
        return (await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken))!;
    }

    public async Task<User?> GetByLogin(string login, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Name == login || u.Email == login, cancellationToken);
    }
}