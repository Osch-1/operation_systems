namespace minimizer.Automate.Mealy
{
    public class StateInfo : IEquatable<StateInfo>
    {
        private readonly string _name;

        public string Name => _name;

        public StateInfo( string name )
        {
            _name = name;
        }

        public bool Equals( StateInfo other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( other, this ) )
            {
                return true;
            }

            return _name.Equals( other._name );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as StateInfo );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _name );
        }
    }
}