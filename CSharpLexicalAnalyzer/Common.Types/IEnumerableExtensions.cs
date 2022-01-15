namespace Common.Types;

public static class IEnumerableExtensions
{
    public static bool IsNullOrEmpty<T>( this IEnumerable<T> enumeration )
    {
        return enumeration is null
            || !enumeration.Any();
    }
}
