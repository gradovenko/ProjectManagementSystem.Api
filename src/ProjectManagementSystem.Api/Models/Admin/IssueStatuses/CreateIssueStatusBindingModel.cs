using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.IssueStatuses;

public sealed record CreateIssueStatusBindingModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}

public sealed class CreateIssueStatusBindingModelValidator : AbstractValidator<CreateIssueStatusBindingModel>
{
    public CreateIssueStatusBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Name)
            .NotEmpty();
        RuleFor(b => b.IsActive)
            .NotNull();
    }
}