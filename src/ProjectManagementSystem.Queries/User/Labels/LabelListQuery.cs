namespace ProjectManagementSystem.Queries.User.Labels;

public sealed record LabelListQuery(int Offset, int Limit) : PageQuery<LabelListItemViewModel>(Offset, Limit);