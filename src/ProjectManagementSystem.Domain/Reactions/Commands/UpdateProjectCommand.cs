using MediatR;

namespace ProjectManagementSystem.Domain.Reactions.Commands;

public sealed record UpdateReactionCommand : IRequest<UpdateReactionCommandResultState>
{
    public Guid ReactionId { get; init; }
    public string Emoji { get; init; } = null!;
    public string Name { get; init; } = null!;
    public EmojiCategory Category { get; init; }
}

public enum UpdateReactionCommandResultState
{
    ReactionNotFound,
    ReactionWithSameEmojiAlreadyExists,
    ReactionWithSameNameAlreadyExists,
    ReactionUpdated
}