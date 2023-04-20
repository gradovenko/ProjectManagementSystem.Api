using MediatR;

namespace ProjectManagementSystem.Domain.Reactions.Commands;

public sealed record CreateReactionCommand : IRequest<CreateReactionCommandResultState>
{
    /// <summary>
    /// Reaction identifier
    /// </summary>
    public Guid ReactionId { get; init; }
    
    /// <summary>
    /// Reaction emoji
    /// </summary>
    public string Emoji { get; init; } = null!;
    
    /// <summary>
    /// Emoji name
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// SmileysAndEmotion, PeopleAndBody, AnimalsAndNature, FoodAndDrink, TravelAndPlaces,
    /// Activities, Objects, SymbolsAndSigns, Flags, SkinTones, Gender
    /// </summary>
    public EmojiCategory Category { get; init; }
}

public enum CreateReactionCommandResultState
{
    ReactionWithSameEmojiAlreadyExists,
    ReactionWithSameNameAlreadyExists,
    ReactionWithSameIdButOtherParamsAlreadyExists,
    ReactionCreated
}