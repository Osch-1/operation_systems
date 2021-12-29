namespace minimizer.Automate.Mealy
{
    public class Output : IEquatable<Output>
    {
        private readonly string _name;

        public string Name => _name;

        public Output( string name )
        {
            _name = name;
        }

        public bool Equals( Output other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _name.Equals( other._name );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as Output );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _name );
        }

        public override string ToString()
        {
            return $"{_name}";
        }
    }
}
