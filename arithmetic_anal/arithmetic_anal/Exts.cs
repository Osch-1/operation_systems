namespace arithmetic_anal
{
    public static class Exts
    {
        public static IEnumerable<String> AsStringList( this string str )
        {
            return str.Select( c => c.ToString() );
        }
    }
}
