namespace operationSystemLecser
{
    public class Connection : IEquatable<Connection>
    {
        public Connection( string from, string to, string by )
        {
            From = from.Sort();
            To = to.Sort();
            By = by;
        }

        public Connection() { }

        public string From { get; set; }
        public string To { get; set; }
        public string By { get; set; }


        public override int GetHashCode()
        {
            return HashCode.Combine( From, To, By );
        }

        public bool Equals( Connection? other )
        {
            return other is Connection connection &&
                     From == connection.From &&
                     To == connection.To &&
                     By == connection.By;
        }

        public override string ToString()
        {
            return $"{From}({By})->{To}";
        }
    }

}
