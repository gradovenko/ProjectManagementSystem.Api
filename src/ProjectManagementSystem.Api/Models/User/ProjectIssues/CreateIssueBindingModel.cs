using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.ProjectIssues;

public sealed record CreateIssueBindingModel
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public Guid AuthorId { get; init; }
    public IEnumerable<Guid>? AssigneeIds { get; init; } = null!;
    public IEnumerable<Guid>? LabelIds { get; init; } = null!;
    public DateTime? DueDate { get; init; }
}

public sealed class CreateIssueBindingModelValidatorModel : AbstractValidator<CreateIssueBindingModel>
{
    public CreateIssueBindingModelValidatorModel()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Title)
            .NotEmpty();
        RuleFor(b => b.Description)
            .NotNull();
        // RuleFor(b => b.StartDate)
        //     .NotEmpty()
        //     .When(b => b.StartDate != null);
        // RuleFor(b => b.DueDate)
        //     .NotEmpty()
        //     .When(b => b.DueDate != null);
        // RuleFor(b => b.TrackerId)
        //     .NotEmpty();
        // RuleFor(b => b.StatusId)
        //     .NotEmpty();
        // RuleFor(b => b.PriorityId)
        //     .NotEmpty();
        // RuleFor(b => b.AssigneeId)
        //     .NotEmpty()
        //     .When(b => b.AssigneeId != null);
    }
}