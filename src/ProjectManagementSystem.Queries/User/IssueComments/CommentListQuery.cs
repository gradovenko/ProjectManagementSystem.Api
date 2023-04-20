namespace ProjectManagementSystem.Queries.User.IssueComments;

public sealed record CommentListQuery(Guid IssueId, int Offset, int Limit) : PageQuery<CommentListItemViewModel>(Offset, Limit);