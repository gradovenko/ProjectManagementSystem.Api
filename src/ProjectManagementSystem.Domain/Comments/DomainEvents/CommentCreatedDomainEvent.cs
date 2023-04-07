using MediatR;

namespace ProjectManagementSystem.Domain.Comments.DomainEvents;

public sealed record CommentCreatedDomainEvent : INotification
{
    public Guid CommentId { get; init; }
    public string Content { get; init; } = null!;
}