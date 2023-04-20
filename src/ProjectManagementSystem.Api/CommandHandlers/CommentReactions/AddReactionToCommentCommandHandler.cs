using MediatR;
using ProjectManagementSystem.Domain.Comments;
using ProjectManagementSystem.Domain.Comments.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.CommentReactions;

public sealed class AddReactionToCommentCommandHandler : IRequestHandler<AddReactionToCommentCommand, AddReactionToCommentCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IReactionGetter _reactionGetter;
    private readonly ICommentUserReactionGetter _commentUserReactionGetter;
    private readonly ICommentRepository _commentRepository;

    public AddReactionToCommentCommandHandler(IUserGetter userGetter, IReactionGetter reactionGetter,
        ICommentUserReactionGetter commentUserReactionGetter, ICommentRepository commentRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
        _commentUserReactionGetter = commentUserReactionGetter ??
                                     throw new ArgumentNullException(nameof(commentUserReactionGetter));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<AddReactionToCommentCommandResultState> Handle(AddReactionToCommentCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return AddReactionToCommentCommandResultState.UserNotFound;
        
        Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);

        if (comment == null)
            return AddReactionToCommentCommandResultState.CommentNotFound;

        Reaction? reaction = await _reactionGetter.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return AddReactionToCommentCommandResultState.ReactionNotFound;

        CommentUserReaction? issueUserReaction = await _commentUserReactionGetter.Get(request.CommentId, request.UserId,
            request.ReactionId, cancellationToken);

        if (issueUserReaction != null)
            return AddReactionToCommentCommandResultState.CommentUserReactionAlreadyExists;

        comment.AddUserReaction(user.Id, reaction.Id);

        await _commentRepository.Save(comment, cancellationToken);

        return AddReactionToCommentCommandResultState.ReactionToCommentAdded;
    }
}