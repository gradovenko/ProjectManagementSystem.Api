using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Queries.User.Issues;

namespace ProjectManagementSystem.Queries.Infrastructure.User.Issues
{
    public sealed class IssueListQueryHandler : IRequestHandler<IssueListQuery, Page<IssueListItemView>>
    {
        private readonly IssueDbContext _context;

        public IssueListQueryHandler(IssueDbContext context)
        {
            _context = context;
        }

        public async Task<Page<IssueListItemView>> Handle(IssueListQuery query, CancellationToken cancellationToken)
        {
            var sql = _context.Issues.AsNoTracking()
                .OrderBy(p => p.CreateDate)
                .Select(i => new IssueListItemView
                {
                    Id = i.Id,
                    Number = i.Number,
                    Title = i.Title,
                    TrackerName = i.Tracker.Name,
                    StatusName = i.Status.Name,
                    PriorityName = i.Priority.Name,
                    AssigneeName = i.Assignee.Name,
                    UpdateDate = i.UpdateDate ?? i.CreateDate,
                })
                .AsQueryable();

            return new Page<IssueListItemView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await sql.CountAsync(cancellationToken),
                Items = await sql.Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken)
            };
        }
    }
}