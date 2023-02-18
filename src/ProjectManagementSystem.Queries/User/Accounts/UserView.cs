namespace ProjectManagementSystem.Queries.User.Accounts;

public sealed record UserView
{
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
        
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }
}