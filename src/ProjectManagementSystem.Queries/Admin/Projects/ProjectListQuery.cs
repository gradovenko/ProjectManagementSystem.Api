namespace ProjectManagementSystem.Queries.Admin.Projects;

public sealed record ProjectListQuery(int Offset, int Limit) : PageQuery<ProjectListItemView>(Offset, Limit);