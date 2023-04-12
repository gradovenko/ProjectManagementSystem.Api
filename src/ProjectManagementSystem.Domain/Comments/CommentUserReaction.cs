namespace ProjectManagementSystem.Domain.Comments;

public sealed class CommentUserReaction
{
    public Guid CommentUserReactionId { get; init; }
    public Guid CommentId { get; init; }
    //public Comment Comment { get; init; } = null!;
    public Guid UserId { get; init; }
    //public User User { get; init; } = null!;
    public Guid ReactionId { get; init; }
    //public Reaction Reaction { get; init; } = null!;

    public CommentUserReaction(Guid commentId, Guid userId, Guid reactionId)
    {
        CommentUserReactionId = Guid.NewGuid();
        CommentId = commentId;
        UserId = userId;
        ReactionId = reactionId;
    }
}