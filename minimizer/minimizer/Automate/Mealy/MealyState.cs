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

            return _name.Equals( other._name )
                || _signalsToActions.Equals( other._signalsToActions );
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
