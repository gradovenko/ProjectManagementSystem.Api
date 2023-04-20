using FluentValidation;

namespace ProjectManagementSystem.Api.Models.User.CommentReactions;

public sealed record AddReactionToCommentBindingModel
{
    /// <summary>
    /// Reaction id
    /// </summary>
    public Guid Id { get; set; }
}

public sealed class AddReactionToCommentBindingModelValidator : AbstractValidator<AddReactionToCommentBindingModel>
{
    public AddReactionToCommentBindingModelValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty();
    }
}