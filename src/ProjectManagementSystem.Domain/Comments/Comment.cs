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
        if (childComment.ParentComment != null)
            throw new InvalidOperationException("The comment is already a child comment");

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

    public void AddUserReaction(Guid userId, Guid reactionId)
    {
        if (_commentUserReactions.Any(o => o.UserId == userId && o.ReactionId == reactionId))
            throw new InvalidOperationException($"User {userId} already added this reaction {reactionId} to the issue {Id}");

        _commentUserReactions.Add(new CommentUserReaction(Id, userId, reactionId));

        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }

    public void RemoveUserReaction(Guid userId, Guid reactionId)
    {
        CommentUserReaction? commentUserReaction =
            _commentUserReactions.Single(o => o.UserId == userId && o.ReactionId == reactionId);

        if (commentUserReaction == null)
            throw new InvalidOperationException($"User {userId} has already removed this reaction {reactionId} from the issue, or the issue user reaction {reactionId} does not exist");

        _commentUserReactions.Remove(commentUserReaction);

        UpdateDate = DateTime.UtcNow;
        _concurrencyToken = Guid.NewGuid();
    }
}