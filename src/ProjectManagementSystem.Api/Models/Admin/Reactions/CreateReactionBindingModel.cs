using ProjectManagementSystem.Domain.Reactions;

namespace ProjectManagementSystem.Api.Models.Admin.Reactions;

public sealed record CreateReactionBindingModel
{
    /// <summary>
    /// Reaction identifier
    /// </summary>
    public Guid Id { get; init; }
    
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