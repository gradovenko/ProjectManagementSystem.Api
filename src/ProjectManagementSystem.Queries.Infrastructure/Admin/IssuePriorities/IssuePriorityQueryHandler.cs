using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventFlow.Queries;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public class IssuePriorityQueryHandler : IQueryHandler<IssuePriorityQuery, IssuePriorityView>
    {
        private readonly IssuePriorityDbContext _context;

        public IssuePriorityQueryHandler(IssuePriorityDbContext context)
        {
            _context = context;
        }

        public async Task<IssuePriorityView> ExecuteQueryAsync(IssuePriorityQuery query, CancellationToken cancellationToken)
        {
            return await _context.IssuePriorities.AsNoTracking()
                .Where(issuePriority => issuePriority.Id == query.Id)
                .Select(issuePriority => new IssuePriorityView
                {
                    Id = issuePriority.Id,
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}