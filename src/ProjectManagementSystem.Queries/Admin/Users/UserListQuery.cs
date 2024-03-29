namespace ProjectManagementSystem.Queries.Admin.Users;

public sealed record UserListQuery(int Offset, int Limit) : PageQuery<UserListItemViewModel>(Offset, Limit);