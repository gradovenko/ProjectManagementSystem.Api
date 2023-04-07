namespace ProjectManagementSystem.Domain.Issues;

public interface ILabelGetter
{
    Task<Label?> Get(Guid id, CancellationToken cancellationToken);
}