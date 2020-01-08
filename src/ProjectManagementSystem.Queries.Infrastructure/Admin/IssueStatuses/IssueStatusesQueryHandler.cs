using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssueStatuses;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses
{
    public sealed class IssueStatusesQueryHandler : IRequestHandler<IssueStatusesQuery, Page<FullIssueStatusView>>
    {
        private readonly IssueStatusDbContext _context;

        public IssueStatusesQueryHandler(IssueStatusDbContext context)
        {
            _context = context;
        }

        public async Task<Page<FullIssueStatusView>> Handle(IssueStatusesQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.IssueStatuses.AsNoTracking()
                .Select(issuePriority => new FullIssueStatusView
                {
                    Id = issuePriority.Id,
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                });

            return new Page<FullIssueStatusView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToArrayAsync(cancellationToken)
            };
        }
    }
}