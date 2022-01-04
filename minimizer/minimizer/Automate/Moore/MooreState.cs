namespace minimizer.Automate.Moore
{
    public class MooreState : IState, IEquatable<MooreState>
    {
        private readonly string _name;
        private readonly Output _output;
        private SignalsToActions<MooreAction> _signalsToActions = new();

        public string Name => _name;
        public Output Output => _output;
        public SignalsToActions<MooreAction> SignalsToActions => _signalsToActions;

        public MooreState( string name, Output output )
        {
            _name = name;
            _output = output;
        }

        public void SetSignalsToActions( SignalsToActions<MooreAction> signalsToActions )
        {
            _signalsToActions = signalsToActions;
        }

        public bool Equals( MooreState other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _name == other._name
                || _output == other._output
                && _signalsToActions.Equals( other._signalsToActions );
        }

        public bool Equals( IState state )
        {
            return Equals( state as MooreState );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as MooreState );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _output, _signalsToActions );
        }

        public override string ToString()
        {
            return $"{Name}/{Output}";
        }
    }
}