namespace minimizer.Automate.Mealy
{
    public class MealyState : IState, IEquatable<MealyState>
    {
        private readonly string _name;
        private SignalsToActions<MealyAction> _signalsToActions = new();

        public string Name => _name;
        public SignalsToActions<MealyAction> SignalsToActions => _signalsToActions;

        public MealyState( string name )
        {
            _name = name;
        }

        public void SetSignalsToActions( SignalsToActions<MealyAction> signalsToActions )
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

            return _signalsToActions.Equals( other._signalsToActions );
        }

        public bool Equals( IState state )
        {
            return Equals( state as MealyState );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as MealyState );
        }

        public override string ToString()
        {
            return $"{_name}";
        }

        public override int GetHashCode()
        {
            return _signalsToActions.GetHashCode();
        }
    }
}
