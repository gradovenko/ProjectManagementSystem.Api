namespace ProjectManagementSystem.Queries.Profiles;

public sealed record UserViewModel
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