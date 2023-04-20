using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class ReopenIssueCommandHandler : IRequestHandler<ReopenIssueCommand, ReopenIssueCommandResultState>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IUserGetter _userGetter;

    public ReopenIssueCommandHandler(IIssueRepository issueRepository, IUserGetter userGetter)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
    }

    public async Task<ReopenIssueCommandResultState> Handle(ReopenIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return ReopenIssueCommandResultState.UserNotFound;

        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return ReopenIssueCommandResultState.IssueNotFound;

        issue.Reopen();

        await _issueRepository.Save(issue, cancellationToken);

        return ReopenIssueCommandResultState.IssueReopened;
    }
}