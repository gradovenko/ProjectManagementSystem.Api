namespace ProjectManagementSystem.Api.Models.Admin.Reactions;

public sealed record GetReactionListBindingModel
{
    /// <summary>
    /// Offset
    /// </summary>
    public int Offset { get; set; } = 0;
        
    /// <summary>
    /// Limit
    /// </summary>
    public int Limit { get; set; } = 10;
}