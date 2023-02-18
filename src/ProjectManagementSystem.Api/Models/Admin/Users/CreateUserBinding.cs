using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Users;

public class CreateUserBinding
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}

public class CreateUserBindingValidator : AbstractValidator<CreateUserBinding>
{
    public CreateUserBindingValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Name)
            .NotEmpty();
        RuleFor(b => b.Password)
            .NotEmpty();
        RuleFor(b => b.FirstName)
            .NotEmpty();
        RuleFor(b => b.LastName)
            .NotEmpty();
        RuleFor(b => b.Email)
            .NotEmpty();
        RuleFor(b => b.Role)
            .IsInEnum();
    }
}