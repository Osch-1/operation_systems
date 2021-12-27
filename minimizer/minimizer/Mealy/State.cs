namespace minimizer.Mealy
{
    public class State : IEquatable<State>
    {
        private readonly string _name;

        public string Name => _name;

        public State( string name )
        {
            _name = name;
        }

        public bool Equals( State other )
        {
            if ( other == null )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return other._name == _name;
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as State );
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
