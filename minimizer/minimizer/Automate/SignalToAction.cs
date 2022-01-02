namespace minimizer.Automate
{
    public class SignalToAction<T> : IEquatable<SignalToAction<T>>
        where T : IAction
    {
        private readonly Signal _signal;
        private readonly T _action;

        public Signal Signal => _signal;
        public T Action => _action;

        public SignalToAction( Signal signal, T action )
        {
            _signal = signal;
            _action = action;
        }

        public bool Equals( SignalToAction<T> other )
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
            return Equals( obj as SignalToAction<T> );
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
