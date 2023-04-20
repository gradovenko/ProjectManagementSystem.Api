using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Users;

namespace ProjectManagementSystem.Infrastructure.Users;

public sealed class UserRepository : IUserRepository
{
    private readonly UserDbContext _context;

    public UserRepository(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User?> Get(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }
        
    public async Task<User?> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
    }

    public async Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task Save(User user, CancellationToken cancellationToken)
    {
        if (_context.Entry(user).State == EntityState.Detached)
            await _context.Users.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);        
    }
}