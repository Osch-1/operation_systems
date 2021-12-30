namespace minimizer.Automate
{
    public class Signal : IEquatable<Signal>
    {
        private readonly string _name;

        public string Name => _name;

        public Signal( string name )
        {
            _name = name;
        }

        public bool Equals( Signal other )
        {
            if ( other == null )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return other._name.Equals( _name );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as Signal );
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_name}";
        }
    }
}
