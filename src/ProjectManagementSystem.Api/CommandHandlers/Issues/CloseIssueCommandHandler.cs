using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class CloseIssueCommandHandler : IRequestHandler<CloseIssueCommand, CloseIssueCommandResultState>
{
    private readonly IIssueRepository _issueRepository;
    private readonly IUserGetter _userGetter;

    public CloseIssueCommandHandler(IIssueRepository issueRepository, IUserGetter userGetter)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
    }

    public async Task<CloseIssueCommandResultState> Handle(CloseIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return CloseIssueCommandResultState.UserNotFound;
        
        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return CloseIssueCommandResultState.IssueNotFound;

        issue.Close(request.UserId);

        await _issueRepository.Save(issue, cancellationToken);

        return CloseIssueCommandResultState.IssueClosed;
    }
}