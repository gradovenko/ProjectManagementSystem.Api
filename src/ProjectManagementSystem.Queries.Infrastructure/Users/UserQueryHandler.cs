using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Profiles;

namespace ProjectManagementSystem.Queries.Infrastructure.Users;

public sealed class UserQueryHandler : IRequestHandler<UserQuery, UserViewModel>
{
    private readonly UserDbContext _context;

    public UserQueryHandler(UserDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UserViewModel> Handle(UserQuery query, CancellationToken cancellationToken)
    {
        return (await _context.Users.AsNoTracking()
            .Where(user => user.Id == query.Id)
            .Select(user => new UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
            })
            .SingleOrDefaultAsync(cancellationToken))!;
    }
}