using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Domain.Issues;

namespace ProjectManagementSystem.Infrastructure.Issues;

public sealed class IssueLabelGetter : IIssueLabelGetter
{
    private readonly IssueDbContext _context;

    public IssueLabelGetter(IssueDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IssueLabel?> Get(Guid issueId, Guid labelId, CancellationToken cancellationToken)
    {
        return await _context.IssueLabels
            .AsNoTracking()
            .SingleOrDefaultAsync(o => o.IssueId == issueId && o.LabelId == labelId, cancellationToken);
    }
}