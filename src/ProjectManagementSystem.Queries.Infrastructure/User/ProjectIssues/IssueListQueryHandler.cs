using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.ProjectIssues;

namespace ProjectManagementSystem.Queries.Infrastructure.User.ProjectIssues
{
    public sealed class IssueListQueryHandler : IRequestHandler<IssueListQuery, Page<IssueListView>>
    {
        private readonly IssueDbContext _context;

        public IssueListQueryHandler(IssueDbContext context)
        {
            _context = context;
        }

        public async Task<Page<IssueListView>> Handle(IssueListQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Issues.AsNoTracking()
                .OrderBy(p => p.CreateDate)
                .Where(i => i.ProjectId == query.ProjectId)
                .Select(i => new IssueListView
                {
                    Id = i.Id,
                    Index = i.Index,
                    Title = i.Title,
                    TrackerName = i.Tracker.Name,
                    StatusName = i.Status.Name,
                    PriorityName = i.Priority.Name,
                    AssigneeName = i.Assignee.Name,
                    UpdateDate = i.UpdateDate ?? i.CreateDate,
                })
                .AsQueryable();

            return new Page<IssueListView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
            };
        }
    }
}