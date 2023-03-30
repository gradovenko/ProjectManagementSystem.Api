using MediatR;

namespace ProjectManagementSystem.Domain.Issues.DomainEvents;

public sealed record TimeEntryCreatedDomainEvent : INotification
{
    public Guid Id { get; init; }
    public string Content { get; init; } = null!;
}