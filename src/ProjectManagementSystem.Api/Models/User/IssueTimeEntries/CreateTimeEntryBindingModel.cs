using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.IssueTimeEntries;

public sealed record CreateTimeEntryBindingModel
{
    /// <summary>
    /// 
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public decimal Hours { get; set; }
        
    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }
        
    /// <summary>
    /// 
    /// </summary>
    public DateTime DueDate { get; set; }
}
    
public sealed class CreateTimeEntryBindingModelValidator : AbstractValidator<CreateTimeEntryBindingModel>
{
    public CreateTimeEntryBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Hours)
            .NotEmpty();
    }
}