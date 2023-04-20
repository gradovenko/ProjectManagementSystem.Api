using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.IssueReactions;

public sealed class AddReactionToIssueCommandHandler : IRequestHandler<AddReactionToIssueCommand, AddReactionToIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IReactionGetter _reactionGetter;
    private readonly IIssueUserReactionGetter _issueUserReactionGetter;
    private readonly IIssueRepository _issueRepository;

    public AddReactionToIssueCommandHandler(IUserGetter userGetter, IReactionGetter reactionGetter,
        IIssueUserReactionGetter issueUserReactionGetter, IIssueRepository issueRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
        _issueUserReactionGetter =
            issueUserReactionGetter ?? throw new ArgumentNullException(nameof(issueUserReactionGetter));
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
    }

    public async Task<AddReactionToIssueCommandResultState> Handle(AddReactionToIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return AddReactionToIssueCommandResultState.UserNotFound;
        
        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return AddReactionToIssueCommandResultState.IssueNotFound;

        Reaction? reaction = await _reactionGetter.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return AddReactionToIssueCommandResultState.ReactionNotFound;

        IssueUserReaction? issueUserReaction = await _issueUserReactionGetter.Get(request.IssueId, request.UserId,
            request.ReactionId, cancellationToken);

        if (issueUserReaction != null)
            return AddReactionToIssueCommandResultState.IssueUserReactionAlreadyExists;

        issue.AddUserReaction(user.Id, reaction.Id);

        await _issueRepository.Save(issue, cancellationToken);

        return AddReactionToIssueCommandResultState.ReactionToIssueAdded;
    }
}