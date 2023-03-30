using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.IssuePriorities;

public sealed record GetIssuePrioritieListBindingModel
{
    /// <summary>
    /// Offset
    /// </summary>
    public int Offset { get; init; } = 0;
        
    /// <summary>
    /// Limit
    /// </summary>
    public int Limit { get; init; } = 10;
}

public sealed class GetIssuePrioritieListBindingModelValidator : AbstractValidator<GetIssuePrioritieListBindingModel>
{
    public GetIssuePrioritieListBindingModelValidator()
    {
        RuleFor(b => b.Offset)
            .GreaterThanOrEqualTo(0);
        RuleFor(b => b.Limit)
            .InclusiveBetween(2, 1000);
    }
}