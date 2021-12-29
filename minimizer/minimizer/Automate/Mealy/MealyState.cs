using minimizer.Automate;

namespace minimizer.Automate.Mealy
{
    public class MealyState : IState, IEquatable<MealyState>
    {
        private readonly string _name;

        private SignalsToActions _signalsToActions;

        public string Name => _name;

        public SignalsToActions SignalsToActions => _signalsToActions;

        public MealyState( string name )
        {
            _name = name;
        }

        public void SetSignalsToActions( SignalsToActions signalsToActions )
        {
            _signalsToActions = signalsToActions;
        }

        public bool Equals( MealyState other )
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
            return Equals( obj as MealyState );
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
