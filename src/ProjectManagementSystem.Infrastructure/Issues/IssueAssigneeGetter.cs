using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class IssueAssigneeGetter : IIssueAssigneeGetter
{
    private readonly IssueDbContext _context;

    public IssueAssigneeGetter(IssueDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IssueAssignee?> Get(Guid issueId, Guid assigneeId, CancellationToken cancellationToken)
    {
        return await _context.IssueAssignees
            .AsNoTracking()
            .SingleOrDefaultAsync(o => o.IssueId == issueId && o.AssigneeId == assigneeId, cancellationToken);
    }
}