using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.IssueLabels;

public sealed record AddLabelToIssueBindingModel
{
    /// <summary>
    /// Label identifier
    /// </summary>
    public Guid Id { get; set; }
}

public sealed class AddReactionToIssueBindingModelValidator : AbstractValidator<AddLabelToIssueBindingModel>
{
    public AddReactionToIssueBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
    }
}