using MediatR;

namespace ProjectManagementSystem.Domain.Issues.Commands;

public sealed record CreateIssueCommand : IRequest<CreateIssueCommandResultState>
{
    public Guid IssueId { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public Guid ProjectId { get; init; }
    public Guid AuthorId { get; init; }
    public IEnumerable<Guid> AssigneeIds { get; init; } = null!;
    public IEnumerable<Guid> LabelIds { get; init; } = null!;
    public DateTime? DueDate { get; init; }
}

public enum CreateIssueCommandResultState
{
    IssueCreated,
    IssueAlreadyExists,
    ProjectNotFound,
    AuthorNotFound,
    AssigneeNotFound,
    LabelNotFound,
}