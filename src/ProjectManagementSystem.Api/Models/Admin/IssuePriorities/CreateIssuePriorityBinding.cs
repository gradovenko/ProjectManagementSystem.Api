using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.IssuePriorities;

public class CreateIssuePriorityBinding
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

public class CreateIssuePriorityValidator : AbstractValidator<CreateIssuePriorityBinding>
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