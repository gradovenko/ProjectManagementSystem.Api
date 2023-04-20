namespace ProjectManagementSystem.Domain.Comments;

public interface ICommentUserReactionGetter
{
    Task<CommentUserReaction?> Get(Guid commentId, Guid userId, Guid reactionId, CancellationToken cancellationToken);
}