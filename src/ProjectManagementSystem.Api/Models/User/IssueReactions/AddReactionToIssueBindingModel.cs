using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.IssueReactions;

public sealed record AddReactionToIssueBindingModel
{
    /// <summary>
    /// 
    /// </summary>
    public string Id { get; set; }
}

public sealed class AddReactionToIssueBindingModelValidator : AbstractValidator<AddReactionToIssueBindingModel>
{
    public AddReactionToIssueBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
    }
}