using MediatR;
using ProjectManagementSystem.Domain.Comments;
using ProjectManagementSystem.Domain.Comments.Commands;

namespace ProjectManagementSystem.Api.CommandHandlers.CommentReactions;

public sealed class RemoveReactionFromCommentCommandHandler : IRequestHandler<RemoveReactionFromCommentCommand, RemoveReactionFromCommentCommandResultState>
{
    private readonly IUserGetter _userGetter;
    private readonly IReactionGetter _reactionGetter;
    private readonly ICommentRepository _commentRepository;
    private readonly ICommentUserReactionGetter _commentUserReactionRepository;

    public RemoveReactionFromCommentCommandHandler(IUserGetter userGetter, IReactionGetter reactionGetter,
        ICommentRepository commentRepository, ICommentUserReactionGetter commentUserReactionRepository)
    {
        _userGetter = userGetter ?? throw new ArgumentNullException(nameof(userGetter));
        _reactionGetter = reactionGetter ?? throw new ArgumentNullException(nameof(reactionGetter));
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _commentUserReactionRepository = commentUserReactionRepository ??
                                       throw new ArgumentNullException(nameof(commentUserReactionRepository));
    }

    public async Task<RemoveReactionFromCommentCommandResultState> Handle(RemoveReactionFromCommentCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userGetter.Get(request.UserId, cancellationToken);

        if (user == null)
            return RemoveReactionFromCommentCommandResultState.UserNotFound;

        Comment? comment = await _commentRepository.Get(request.CommentId, cancellationToken);

        if (comment == null)
            return RemoveReactionFromCommentCommandResultState.CommentNotFound;

        Reaction? reaction = await _reactionGetter.Get(request.ReactionId, cancellationToken);

        if (reaction == null)
            return RemoveReactionFromCommentCommandResultState.ReactionNotFound;

        CommentUserReaction? commentUserReaction = await _commentUserReactionRepository.Get(request.CommentId, request.UserId,
            request.ReactionId, cancellationToken);

        if (commentUserReaction == null)
            return RemoveReactionFromCommentCommandResultState.CommentUserReactionNotFound;

        comment.RemoveUserReaction(user.Id, reaction.Id);

        await _commentRepository.Save(comment, cancellationToken);

        return RemoveReactionFromCommentCommandResultState.ReactionFromCommentRemoved;
    }
}