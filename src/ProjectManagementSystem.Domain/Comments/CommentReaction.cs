namespace ProjectManagementSystem.Domain.Comments;

public sealed class CommentReaction
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid ReactionId { get; private set; }
    public Reaction Reaction { get; private set; } = null!;

    public CommentReaction(User user, Reaction reaction)
    {
        User = user;
        Reaction = reaction;
    }
}