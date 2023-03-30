using FluentValidation;
using ProjectManagementSystem.Domain.Projects;

namespace ProjectManagementSystem.Api.Models.User.Projects;

public sealed class CreateProjectBindingModel
{
    /// <summary>
    /// Project identifier
    /// </summary>
    public Guid Id { get; init; }
        
    /// <summary>
    /// Project name
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; init; }
    
    /// <summary>
    /// Project path
    /// </summary>
    public string Path { get; init; }

    /// <summary>
    /// Project visibility
    /// </summary>
    public ProjectVisibility Visibility { get; init; }
}
    
public sealed class CreateProjectBindingModelValidator : AbstractValidator<CreateProjectBindingModel>
{
    public CreateProjectBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Name)
            .NotEmpty();
        RuleFor(b => b.Path)
            .NotEmpty();
        RuleFor(b => b.Visibility)
            .IsInEnum();
    }
}