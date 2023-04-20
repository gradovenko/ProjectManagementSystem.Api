namespace ProjectManagementSystem.Queries.User.Projects;

public sealed record ProjectListQuery(int Offset, int Limit) : PageQuery<ProjectListItemViewModel>(Offset, Limit);