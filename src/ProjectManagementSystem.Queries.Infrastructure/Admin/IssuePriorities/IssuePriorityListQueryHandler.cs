using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities;

public class IssuePriorityListQueryHandler : IRequestHandler<IssuePriorityListQuery, Page<IssuePriorityListItemView>>
{
    private readonly IssuePriorityDbContext _context;

    public IssuePriorityListQueryHandler(IssuePriorityDbContext context)
    {
        _context = context;
    }

    public async Task<Page<IssuePriorityListItemView>> Handle(IssuePriorityListQuery query,
        CancellationToken cancellationToken)
    {
        var sql = _context.IssuePriorities.AsNoTracking()
            .Select(issuePriority => new IssuePriorityListItemView
            {
                Id = issuePriority.Id,
                Name = issuePriority.Name,
                IsActive = issuePriority.IsActive
            });

        return new Page<IssuePriorityListItemView>
        {
            Limit = query.Limit,
            Offset = query.Offset,
            Total = await sql.CountAsync(cancellationToken),
            Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
        };
    }
}