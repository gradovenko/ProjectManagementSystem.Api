using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.Admin.IssuePriorities;

namespace ProjectManagementSystem.Queries.Infrastructure.Admin.IssuePriorities
{
    public class IssuePriorityQueryHandler : IRequestHandler<IssuePriorityQuery, ShortIssuePriorityView>
    {
        private readonly IssuePriorityDbContext _context;

        public IssuePriorityQueryHandler(IssuePriorityDbContext context)
        {
            _context = context;
        }

        public async Task<ShortIssuePriorityView> Handle(IssuePriorityQuery query, CancellationToken cancellationToken)
        {
            return await _context.IssuePriorities.AsNoTracking()
                .Where(issuePriority => issuePriority.Id == query.Id)
                .Select(issuePriority => new ShortIssuePriorityView
                {
                    Name = issuePriority.Name,
                    IsActive = issuePriority.IsActive
                })
                .SingleOrDefaultAsync(cancellationToken);
        }
    }
}