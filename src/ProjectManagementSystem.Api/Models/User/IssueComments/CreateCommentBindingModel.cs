using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.IssueComments;

public sealed record CreateCommentBindingModel
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
    public Guid? ParentCommentId { get; init; } = null!;
}

public sealed class CreateCommentBindingModelValidatorModel : AbstractValidator<CreateCommentBindingModel>
{
    public CreateCommentBindingModelValidatorModel()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
        RuleFor(b => b.Content)
            .NotNull();
    }
}