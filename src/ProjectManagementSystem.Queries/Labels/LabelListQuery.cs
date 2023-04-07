namespace ProjectManagementSystem.Queries.Labels;

public sealed record LabelListQuery(int Offset, int Limit) : PageQuery<LabelListItemViewModel>(Offset, Limit);