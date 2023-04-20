namespace ProjectManagementSystem.Domain.Labels;

public interface ILabelRepository
{
    Task<Label?> Get(Guid id, CancellationToken cancellationToken);
    Task<Label?> GetByName(string title, CancellationToken cancellationToken);
    Task Save(Label label, CancellationToken cancellationToken);
}