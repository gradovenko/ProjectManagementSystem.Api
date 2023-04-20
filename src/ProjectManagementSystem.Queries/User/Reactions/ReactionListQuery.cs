using MediatR;

namespace ProjectManagementSystem.Queries.User.Reactions;

public sealed record ReactionListQuery() : IRequest<IEnumerable<ReactionListItemViewModel>>;