namespace operationSystemLecser
{
    public static class StringExtensions
    {
        public static string Sort( this string input )
        {
            char[] characters = input.ToArray();
            Array.Sort( characters );
            return new string( characters );
        }
    }
}
