using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.IssueReactions;

public sealed record AddReactionToIssueBindingModel
{
    /// <summary>
    /// Reaction id
    /// </summary>
    public Guid Id { get; set; }
}

public sealed class AddReactionToIssueBindingModelValidator : AbstractValidator<AddReactionToIssueBindingModel>
{
    public AddReactionToIssueBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
    }
}