namespace ProjectManagementSystem.Queries.Admin.Reactions;

public sealed record ReactionListQuery(int Offset, int Limit) : PageQuery<ReactionListItemViewModel>(Offset, Limit);