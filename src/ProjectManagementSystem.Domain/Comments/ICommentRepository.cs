namespace ProjectManagementSystem.Domain.Comments;

public interface ICommentRepository
{
    Task<Comment?> Get(Guid id, CancellationToken cancellationToken);
    Task Save(Comment comment, CancellationToken cancellationToken);
}