using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class UpdateIssueCommandHandler : IRequestHandler<UpdateIssueCommand, UpdateIssueCommandResultState>
{
    private readonly IIssueRepository _issueRepository;

    public UpdateIssueCommandHandler(IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
    }

    public async Task<UpdateIssueCommandResultState> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return UpdateIssueCommandResultState.IssueNotFound;

        issue.Update(request.Title, request.Description);

        await _issueRepository.Save(issue, cancellationToken);

        return UpdateIssueCommandResultState.IssueUpdated;
    }
}