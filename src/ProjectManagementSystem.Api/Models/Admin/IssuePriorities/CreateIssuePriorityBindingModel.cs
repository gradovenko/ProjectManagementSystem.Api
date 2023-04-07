using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.IssuePriorities;

public sealed record CreateIssuePriorityBindingModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
}

public class CreateIssuePriorityValidator : AbstractValidator<CreateIssuePriorityBindingModel>
{
    public CreateIssuePriorityValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Name)
            .NotEmpty();
        RuleFor(b => b.IsActive)
            .NotNull();
    }
}