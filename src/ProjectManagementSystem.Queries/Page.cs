namespace ProjectManagementSystem.Queries;

public sealed record Page<T> where T : class
{
    public int Total { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public IEnumerable<T> Items { get; set; }
}