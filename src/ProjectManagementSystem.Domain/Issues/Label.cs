namespace ProjectManagementSystem.Domain.Issues;

public sealed class Label
{
    public Guid Id { get; private set; }
    //
    // private List<IssueLabel> _issueLabels = new();
    // public IReadOnlyCollection<IssueLabel> IssueLabels => _issueLabels.AsReadOnly();

    private Label() { }
}