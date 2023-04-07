namespace ProjectManagementSystem.Queries.Projects;

public sealed record ProjectListQuery(int Offset, int Limit) : PageQuery<ProjectListItemViewModel>(Offset, Limit);