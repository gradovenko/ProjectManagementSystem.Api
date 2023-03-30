using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class CloseIssueCommandHandler : IRequestHandler<CloseIssueCommand, CloseIssueCommandResultState>
{
    private readonly IIssueRepository _issueRepository;

    public CloseIssueCommandHandler(IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
    }

    public async Task<CloseIssueCommandResultState> Handle(CloseIssueCommand request, CancellationToken cancellationToken)
    {
        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return CloseIssueCommandResultState.IssueNotFound;

        issue.Close(request.UserId);

        await _issueRepository.Save(issue, cancellationToken);

        return CloseIssueCommandResultState.IssueClosed;
    }
}