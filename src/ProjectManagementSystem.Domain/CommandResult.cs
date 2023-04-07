namespace ProjectManagementSystem.Domain;

public sealed record CommandResult<T1, T2>
    where T1 : class
    where T2 : Enum
{
    public T1? Value { get; init; }
    public T2 State { get; init; } = default!;
}