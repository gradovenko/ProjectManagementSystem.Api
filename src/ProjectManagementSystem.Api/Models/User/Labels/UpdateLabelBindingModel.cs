using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.Labels;

public sealed record UpdateLabelBindingModel
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string BackgroundColor { get; init; } = null!;
}
    
public sealed class UpdateLabelBindingModelValidator : AbstractValidator<CreateProjectLabelBindingModel>
{
    public UpdateLabelBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
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