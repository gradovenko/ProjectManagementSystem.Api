using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Trackers;

public class CreateTrackerBinding
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
    
public class CreateTrackerBindingValidator : AbstractValidator<CreateTrackerBinding>
{
    public CreateTrackerBindingValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Name)
            .NotEmpty();
    }
}