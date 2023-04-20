namespace ProjectManagementSystem.Domain.Comments;

public sealed class CommentUserReaction
{
    public Guid CommentId { get; init; }
    public Guid UserId { get; init; }
    public Guid ReactionId { get; init; }
    
    private CommentUserReaction() { }

    public CommentUserReaction(Guid commentId, Guid userId, Guid reactionId)
    {
        CommentId = commentId;
        UserId = userId;
        ReactionId = reactionId;
    }
}