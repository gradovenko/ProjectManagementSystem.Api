using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssueStatuses;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssueStatuses
{
    public class IssueStatusQueryHandler : IRequestHandler<IssueStatusQuery, ShortIssueStatusView>
    {
        private readonly IssueStatusDbContext _context;

        public IssueStatusQueryHandler(IssueStatusDbContext context)
        {
            _context = context;
        }

        public async Task<ShortIssueStatusView> Handle(IssueStatusQuery query, CancellationToken cancellationToken)
        {
            return await _context.IssueStatuses.AsNoTracking()
                .Where(issuePriority => issuePriority.Id == query.Id)
                .Select(issuePriority => new ShortIssueStatusView
                {
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}