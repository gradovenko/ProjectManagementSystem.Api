 namespace ProjectManagementSystem.Domain.Reactions;

public sealed class Reaction
{
    public Guid Id { get; private set; }
    public string Emoji { get; private set; }
    public string Name { get; private set; }
    public EmojiCategory Category { get; private set; }
    public bool IsDeleted { get; private set; }
    private Guid _concurrencyToken;

    private Reaction() { }

    public Reaction(Guid id, string emoji, string name, EmojiCategory category)
    {
        Id = id;
        Emoji = emoji ?? throw new ArgumentNullException(nameof(emoji));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Category = category;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Update(string emoji, string name, EmojiCategory category)
    {
        Emoji = emoji;
        Name = name;
        Category = category;
        _concurrencyToken = Guid.NewGuid();
    }

    public void Delete()
    {
        IsDeleted = true;
        _concurrencyToken = Guid.NewGuid();
    }
}