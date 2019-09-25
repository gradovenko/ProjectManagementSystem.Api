using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public class IssuePrioritiesQueryHandler : IRequestHandler<IssuePrioritiesQuery, Page<FullIssuePriorityView>>
    {
        private readonly IssuePriorityDbContext _context;

        public IssuePrioritiesQueryHandler(IssuePriorityDbContext context)
        {
            _context = context;
        }

        public async Task<Page<FullIssuePriorityView>> Handle(IssuePrioritiesQuery query,
            CancellationToken cancellationToken)
        {
            var sql = _context.IssuePriorities.AsNoTracking()
                .Select(issuePriority => new FullIssuePriorityView
                {
                    Id = issuePriority.Id,
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                });

            return new Page<FullIssuePriorityView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToArrayAsync(cancellationToken)
            };
        }
    }
}