namespace ProjectManagementSystem.DatabaseMigrations.Entities;

public sealed class Project
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsPrivate { get; set; }
    public string Status { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public Guid ConcurrencyStamp { get; set; }
}