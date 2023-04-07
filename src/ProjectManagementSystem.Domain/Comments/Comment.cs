using MediatR;
using ProjectManagementSystem.Domain.Comments.DomainEvents;

namespace ProjectManagementSystem.Domain.Comments;

public sealed class Comment
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    private List<Reaction> _reactions = new();
    public IReadOnlyCollection<Reaction> Reactions => _reactions.AsReadOnly();
    private List<INotification> _domainEvents = new();
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
    private Guid _concurrencyToken;

    public Comment(Guid id, string content)
    {
        Id = id;
        Content = content;
        IsDeleted = false;
        CreateDate = UpdateDate = DateTime.UtcNow;

        _domainEvents.Add(new CommentCreatedDomainEvent
        {
            CommentId = id,
            Content = content
        });

        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string content)
    {
        Content = content;
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
    
    public void Delete()
    {
        IsDeleted = true;
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void AddReaction(Reaction reaction)
    {
        _reactions.Add(reaction);
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}