using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class RemoveReactionFromIssueCommandHandler : IRequestHandler<RemoveReactionFromIssueCommand, RemoveReactionFromIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IReactionGetter _reactionGetter;
    private readonly IIssueRepository _issueRepository;

    public RemoveReactionFromIssueCommandHandler(IUserGetter userGetter, IReactionGetter reactionGetter,
        IIssueRepository issueRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
    }

    public async Task<RemoveReactionFromIssueCommandResultState> Handle(RemoveReactionFromIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return RemoveReactionFromIssueCommandResultState.UserNotFound;
        
        Reaction? reaction = await _reactionGetter.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return RemoveReactionFromIssueCommandResultState.ReactionNotFound;
        
        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return RemoveReactionFromIssueCommandResultState.IssueNotFound;

        issue.RemoveReaction(reaction);

        await _issueRepository.Save(issue, cancellationToken);

        return RemoveReactionFromIssueCommandResultState.ReactionFromIssueRemoved;
    }
}