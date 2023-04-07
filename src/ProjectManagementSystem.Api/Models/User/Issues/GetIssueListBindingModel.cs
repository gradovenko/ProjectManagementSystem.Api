using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.Issues;

public sealed record GetIssueListBindingModel
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

public sealed class GetIssueListBindingModelValidator : AbstractValidator<GetIssueListBindingModel>
{
    public GetIssueListBindingModelValidator()
    {
        RuleFor(b => b.Offset)
            .GreaterThanOrEqualTo(0);
        RuleFor(b => b.Limit)
            .InclusiveBetween(2, 1000);
    }
}