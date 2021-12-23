namespace lw1
{
    public static class ObjectExtensions
    {
        public static T To<T>( this object? obj )
        {
            if ( obj is null )
            {
                throw new ArgumentNullException( nameof( obj ) );
            }

            Type type = typeof( T );

            if ( Enum.IsDefined( type, obj ) )
            {
                return ( T )Enum.Parse( type, obj.ToString() );
            }

            throw new ArgumentException( $"{obj} is not a value from {type} enumeration" );
        }
    }
}
