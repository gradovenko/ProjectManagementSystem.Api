using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Profiles;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Accounts;

public sealed class UserQueryHandler : IRequestHandler<UserQuery, UserViewModel>
{
    private readonly UserDbContext _context;

    public UserQueryHandler(UserDbContext context)
    {
        _context = context;
    }

    public async Task<UserViewModel> Handle(UserQuery query, CancellationToken cancellationToken)
    {
        return await _context.Users.AsNoTracking()
            .Where(user => user.Id == query.Id)
            .Select(user => new UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
            })
            .SingleOrDefaultAsync(cancellationToken);
    }
}