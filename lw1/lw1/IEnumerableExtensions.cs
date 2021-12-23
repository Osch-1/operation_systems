namespace lw1
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>( this IEnumerable<T> ts )
        {
            return ts.Select( ( t, index ) => (t, index) );
        }
    }
}
