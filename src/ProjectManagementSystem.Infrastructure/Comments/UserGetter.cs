using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Comments;

namespace ProjectManagementSystem.Infrastructure.Comments;

public sealed class UserGetter : IUserGetter
{
    private readonly CommentDbContext _context;

    public UserGetter(CommentDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User?> Get(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}