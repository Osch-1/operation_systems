namespace minimizer.Mealy
{
    public class SignalToAction : IEquatable<SignalToAction>
    {
        private readonly Signal _signal;
        private readonly Action _action;

        public Signal Signal => _signal;
        public Action Action => _action;

        public SignalToAction( Signal signal, Action action )
        {
            _signal = signal;
            _action = action;
        }

        public bool Equals( SignalToAction other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return _signal.Equals( other._signal )
                && _action.Equals( other._action );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as SignalToAction );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _signal, _action );
        }

        public override string ToString()
        {
            return $"{_signal}->{_action}";
        }
    }
}
