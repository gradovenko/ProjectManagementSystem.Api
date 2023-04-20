using MediatR;

namespace ProjectManagementSystem.Queries.User.IssueReactions;

public sealed record ReactionListQuery(Guid IssueId) : IRequest<IEnumerable<ReactionListItemViewModel>>;