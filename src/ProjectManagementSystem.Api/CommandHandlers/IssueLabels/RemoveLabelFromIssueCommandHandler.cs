using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.IssueLabels;

public sealed class RemoveLabelFromIssueCommandHandler : IRequestHandler<RemoveLabelFromIssueCommand, RemoveLabelFromIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly ILabelGetter _labelGetter;
    private readonly IIssueLabelGetter _issueLabelGetter;
    private readonly IIssueRepository _issueRepository;

    public RemoveLabelFromIssueCommandHandler(IUserGetter userGetter, ILabelGetter labelGetter,
        IIssueRepository issueRepository, IIssueLabelGetter issueLabelGetter)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _labelGetter = labelGetter ?? throw new ArgumentNullException(nameof(labelGetter));
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _issueLabelGetter = issueLabelGetter ??
                                       throw new ArgumentNullException(nameof(issueLabelGetter));
    }

    public async Task<RemoveLabelFromIssueCommandResultState> Handle(RemoveLabelFromIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return RemoveLabelFromIssueCommandResultState.UserNotFound;

        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return RemoveLabelFromIssueCommandResultState.IssueNotFound;

        Label? label = await _labelGetter.Get(request.LabelId, cancellationToken);

        if (label == null)
            return RemoveLabelFromIssueCommandResultState.LabelNotFound;

        IssueLabel? issueLabel = await _issueLabelGetter.Get(request.IssueId, request.LabelId, cancellationToken);

        if (issueLabel == null)
            return RemoveLabelFromIssueCommandResultState.IssueUserLabelNotFound;

        issue.RemoveLabel(user.Id, label.Id);

        await _issueRepository.Save(issue, cancellationToken);

        return RemoveLabelFromIssueCommandResultState.LabelFromIssueRemoved;
    }
}