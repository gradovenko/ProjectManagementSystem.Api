using FluentValidation;
using ProjectManagementSystem.Domain.Projects;

namespace ProjectManagementSystem.Api.Models.User.Projects;

public sealed record UpdateProjectBindingModel
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string Path { get; init; } = null!;
    public ProjectVisibility Visibility { get; init; }
}
    
public sealed class UpdateProjectBindingModelValidator : AbstractValidator<UpdateProjectBindingModel>
{
    public UpdateProjectBindingModelValidator()
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