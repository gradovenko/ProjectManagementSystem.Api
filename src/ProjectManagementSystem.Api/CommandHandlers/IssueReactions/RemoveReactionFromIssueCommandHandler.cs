using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.IssueReactions;

public sealed class RemoveReactionFromIssueCommandHandler : IRequestHandler<RemoveReactionFromIssueCommand, RemoveReactionFromIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IReactionGetter _reactionGetter;
    private readonly IIssueRepository _issueRepository;
    private readonly IIssueUserReactionGetter _issueUserReactionRepository;

    public RemoveReactionFromIssueCommandHandler(IUserGetter userGetter, IReactionGetter reactionGetter,
        IIssueRepository issueRepository, IIssueUserReactionGetter issueUserReactionRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
        _issueUserReactionRepository = issueUserReactionRepository ??
                                       throw new ArgumentNullException(nameof(issueUserReactionRepository));
    }

    public async Task<RemoveReactionFromIssueCommandResultState> Handle(RemoveReactionFromIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return RemoveReactionFromIssueCommandResultState.UserNotFound;

        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return RemoveReactionFromIssueCommandResultState.IssueNotFound;

        Reaction? reaction = await _reactionGetter.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return RemoveReactionFromIssueCommandResultState.ReactionNotFound;

        IssueUserReaction? issueUserReaction = await _issueUserReactionRepository.Get(request.IssueId, request.UserId,
            request.ReactionId, cancellationToken);

        if (issueUserReaction == null)
            return RemoveReactionFromIssueCommandResultState.IssueUserReactionNotFound;

        issue.RemoveUserReaction(user.Id, reaction.Id);

        await _issueRepository.Save(issue, cancellationToken);

        return RemoveReactionFromIssueCommandResultState.ReactionFromIssueRemoved;
    }
}