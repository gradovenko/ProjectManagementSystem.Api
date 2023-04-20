using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.Issues;

public sealed record UpdateIssueBindingModel
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
}
    
public sealed class UpdateIssueBindingModelValidator : AbstractValidator<UpdateIssueBindingModel>
{
    public UpdateIssueBindingModelValidator()
    {
        // RuleFor(b => b.Id)
        //     .NotEmpty();
        // RuleFor(b => b.Hours)
        //     .NotEmpty();
        // RuleFor(b => b.Description)
        //     .NotNull();
        // RuleFor(b => b.DueDate)
        //     .NotEmpty();
        // RuleFor(b => b.ActivityId)
        //     .NotEmpty();
    }
}