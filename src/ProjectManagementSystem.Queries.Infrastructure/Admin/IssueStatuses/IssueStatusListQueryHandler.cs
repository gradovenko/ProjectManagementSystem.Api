using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssueStatuses;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses
{
    public sealed class IssueStatusListQueryHandler : IRequestHandler<IssueStatusListQuery, Page<IssueStatusListItemView>>
    {
        private readonly IssueStatusDbContext _context;

        public IssueStatusListQueryHandler(IssueStatusDbContext context)
        {
            _context = context;
        }

        public async Task<Page<IssueStatusListItemView>> Handle(IssueStatusListQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.IssueStatuses.AsNoTracking()
                .Select(issuePriority => new IssueStatusListItemView
                {
                    Id = issuePriority.Id,
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                });

            return new Page<IssueStatusListItemView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
            };
        }
    }
}