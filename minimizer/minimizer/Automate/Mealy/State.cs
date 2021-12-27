using minimizer.Automate;

namespace minimizer.Automate.Mealy
{
    public class State : IEquatable<State>
    {
        private readonly string _name;

        private SignalsToActions _signalsToActions;

        public string Name => _name;

        public State( string name )
        {
            _name = name;
        }

        public void SetSignalsToActions( SignalsToActions signalsToActions )
        {
            _signalsToActions = signalsToActions;
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

            return other._name.Equals( _name )
                && other._signalsToActions.Equals( _signalsToActions );
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
