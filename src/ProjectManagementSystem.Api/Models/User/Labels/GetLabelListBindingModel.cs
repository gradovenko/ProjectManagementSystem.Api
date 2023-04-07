using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.Labels;

public sealed class GetLabelListBindingModel
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

public sealed class GetLabelListBindingModelValidator : AbstractValidator<GetLabelListBindingModel>
{
    public GetLabelListBindingModelValidator()
    {
        RuleFor(b => b.Offset)
            .GreaterThanOrEqualTo(0);
        RuleFor(b => b.Limit)
            .InclusiveBetween(2, 1000);
    }
}