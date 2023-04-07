using FluentValidation;

namespace ProjectManagementSystem.Api.Models.Admin.Users;

public sealed record CreateUserBindingModel
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Password { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public UserRole Role { get; init; }
}

public sealed class CreateUserBindingModelValidator : AbstractValidator<CreateUserBindingModel>
{
    public CreateUserBindingModelValidator()
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