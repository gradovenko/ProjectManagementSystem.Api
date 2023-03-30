namespace ProjectManagementSystem.Domain.Activities;

public sealed class Activity
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public DateTime CreateDate { get; private set; }
    public DateTime UpdateDate { get; private set; }
    private Guid _concurrencyToken;

    private Activity() { }

    public Activity(Guid id, string content)
    {
        Id = id;
        Content = content;
        CreateDate = UpdateDate = DateTime.UtcNow;
    }
}