namespace ProjectManagementSystem.Queries.User.IssueTimeEntries;

public sealed record TimeEntryListItemView
{
    public Guid Id { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public DateTime DueDate { get; set; }
    public string UserName { get; set; }
    public string ActivityName { get; set; }
}