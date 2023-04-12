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
    public Guid AuthorId { get; init; }
    public Guid IssueId { get; private set; }
    public Guid? ParentCommentId { get; private set; }
    public Comment? ParentComment { get; init; }
    private List<Comment> _childComments = new();
    public IReadOnlyCollection<Comment> ChildComments => _childComments.AsReadOnly();
    private List<CommentUserReaction> _commentUserReactions = new();

    public IReadOnlyCollection<CommentUserReaction> CommentUserReactions => _commentUserReactions.AsReadOnly();

    // private List<INotification> _domainEvents = new();
    // public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();
    private Guid _concurrencyToken;

    public Comment(Guid id, string content, Guid authorId, Guid issueId, Guid? parentCommentId)
    {
        Id = id;
        Content = content;
        IsDeleted = false;
        CreateDate = UpdateDate = DateTime.UtcNow;
        AuthorId = authorId;
        IssueId = issueId;
        ParentCommentId = parentCommentId;

        // _domainEvents.Add(new CommentCreatedDomainEvent
        // {
        //     CommentId = id,
        //     Content = content
        // });

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

    public void AddChildComment(Comment childComment)
    {
        _childComments.Add(childComment);
        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    // public void RemoveChildComment(Comment childComment)
    // {
    //     _childComments.Remove(childComment);
    //     UpdateDate = DateTime.UtcNow;
    //     _concurrencyToken = Guid.NewGuid();
    // }

    public void AddReaction(User user, Reaction reaction)
    {
        if (_commentUserReactions.Any(o => o.UserId == user.Id && o.ReactionId == reaction.Id))
            throw new InvalidOperationException("User already added this reaction to the comment");
        _commentUserReactions.Add(new CommentUserReaction(Id, user.Id, reaction.Id));
    }

    public void RemoveReaction(User user, Reaction reaction)
    {
        var reactionToRemove =
            _commentUserReactions.SingleOrDefault(o => o.UserId == user.Id && o.ReactionId == reaction.Id);
        if (reactionToRemove == null)
            throw new InvalidOperationException("User has already removed this reaction from the comment, or the comment user reaction does not exist");
        _commentUserReactions.Remove(reactionToRemove);
    }
}