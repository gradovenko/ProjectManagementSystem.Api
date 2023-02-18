namespace ProjectManagementSystem.Queries.Infrastructure.Extensions;

internal static class EnumExtensions
{
    public static TTo ConvertTo<TTo>(this Enum @enum)
    {
        return (TTo) Enum.Parse(typeof(TTo), @enum.ToString());
    }
}