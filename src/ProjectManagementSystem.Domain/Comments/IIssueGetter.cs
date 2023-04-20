namespace ProjectManagementSystem.Domain.Comments;

public interface IIssueGetter
{
    Task<Issue?> Get(Guid id, CancellationToken cancellationToken);
}