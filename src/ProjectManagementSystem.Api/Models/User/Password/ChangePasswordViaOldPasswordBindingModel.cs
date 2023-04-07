using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.Password;

public sealed record ChangePasswordViaOldPasswordBindingModel
{
    /// <summary>
    /// Old password
    /// </summary>
    public string OldPassword { get; init; }

    /// <summary>
    /// New password
    /// </summary>
    public string NewPassword { get; init; }
}

public sealed class ChangePasswordViaOldPasswordBindingModelValidator : AbstractValidator<ChangePasswordViaOldPasswordBindingModel>
{
    public ChangePasswordViaOldPasswordBindingModelValidator()
    {
        RuleFor(b => b.OldPassword)
            .NotEmpty();
        RuleFor(b => b.NewPassword)
            .MinimumLength(6)
            .NotEqual(b => b.OldPassword);
    }
}