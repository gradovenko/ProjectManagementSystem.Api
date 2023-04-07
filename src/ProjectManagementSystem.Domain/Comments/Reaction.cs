namespace ProjectManagementSystem.Domain.Comments;

public sealed class Reaction
{
    public string Code { get; private set; }

    public Reaction(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }
}