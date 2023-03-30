using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.IssueStatuses;

public sealed record GetIssueStatusListBindingModel
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

public sealed class GetIssueStatusListBindingModelValidator : AbstractValidator<GetIssueStatusListBindingModel>
{
    public GetIssueStatusListBindingModelValidator()
    {
        RuleFor(b => b.Offset)
            .GreaterThanOrEqualTo(0);
        RuleFor(b => b.Limit)
            .InclusiveBetween(2, 1000);
    }
}