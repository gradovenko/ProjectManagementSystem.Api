using MediatR;
using ProjectManagementSystem.Domain.Issues;
using ProjectManagementSystem.Domain.Issues.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.Issues;

public sealed class AddReactionToIssueCommandHandler : IRequestHandler<AddReactionToIssueCommand, AddReactionToIssueCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IReactionGetter _reactionGetter;
    private readonly IIssueRepository _issueRepository;

    public AddReactionToIssueCommandHandler(IUserGetter userGetter, IReactionGetter reactionGetter,
        IIssueRepository issueRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
        _issueRepository = issueRepository ?? throw new ArgumentNullException(nameof(issueRepository));
    }

    public async Task<AddReactionToIssueCommandResultState> Handle(AddReactionToIssueCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return AddReactionToIssueCommandResultState.UserNotFound;
        
        Reaction? reaction = await _reactionGetter.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return AddReactionToIssueCommandResultState.ReactionNotFound;
        
        Issue? issue = await _issueRepository.Get(request.IssueId, cancellationToken);

        if (issue == null)
            return AddReactionToIssueCommandResultState.IssueNotFound;

        issue.AddReaction(reaction);

        await _issueRepository.Save(issue, cancellationToken);

        return AddReactionToIssueCommandResultState.ReactionToIssueAdded;
    }
}