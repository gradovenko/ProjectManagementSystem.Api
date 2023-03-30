using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.Users;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.Users;

public sealed class UserListQueryHandler : IRequestHandler<UserListQuery, PageViewModel<UserListItemViewModel>>
{
    private readonly UserDbContext _context;

    public UserListQueryHandler(UserDbContext context)
    {
        _context = context;
    }
        
    public async Task<PageViewModel<UserListItemViewModel>> Handle(UserListQuery query, CancellationToken cancellationToken)
    {
        var sql = _context.Users.AsNoTracking()
            .Select(user => new UserListItemViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            });

        return new PageViewModel<UserListItemViewModel>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}