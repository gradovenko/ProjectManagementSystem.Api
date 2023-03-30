using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Projects;

public sealed record CreateProjectBindingModel
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public string Name { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public string Description { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public bool IsPrivate { get; init; }
        
    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<Guid> Trackers { get; init; }
}
    
public sealed class CreateProjectBindingModelValidator : AbstractValidator<CreateProjectBindingModel>
{
    public CreateProjectBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Name)
            .NotEmpty();
        RuleFor(b => b.Description)
            .NotNull();
        RuleFor(b => b.IsPrivate)
            .NotNull();
        RuleFor(b => b.Trackers)
            .NotNull();
        RuleForEach(b => b.Trackers)
            .NotEmpty();
    }
}