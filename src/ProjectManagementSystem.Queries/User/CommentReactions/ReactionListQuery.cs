using MediatR;

namespace ProjectManagementSystem.Queries.User.CommentReactions;

public sealed record ReactionListQuery(Guid commentId) : IRequest<IEnumerable<ReactionListItemViewModel>>;