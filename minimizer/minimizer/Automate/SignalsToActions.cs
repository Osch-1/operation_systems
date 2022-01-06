namespace minimizer.Automate
{
    public class SignalsToActions<T> : IEquatable<SignalsToActions<T>>
        where T : IAction
    {
        private readonly List<SignalToAction<T>> _signalToActions = new();

        public IReadOnlyList<SignalToAction<T>> SignalToActions => _signalToActions.ToList();

        public SignalsToActions()
        {
        }

        public SignalsToActions( IEnumerable<SignalToAction<T>> signalToActions )
        {
            _signalToActions = signalToActions.ToList();
        }

        public void AddSignalToAction( SignalToAction<T> signalToAction )
        {
            if ( _signalToActions.Contains( signalToAction ) )
            {
                throw new ArgumentException( $"Current SignalsToActions instance already contains such SignalToAction pair{signalToAction}" );
            }

            _signalToActions.Add( signalToAction );
        }

        public bool Equals( SignalsToActions<T> other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( other, this ) )
            {
                return true;
            }

            foreach ( SignalToAction<T> a in _signalToActions )
            {
                SignalToAction<T> b = other._signalToActions.Where( sta => sta.Signal.Equals( a.Signal ) ).First();
                if ( !a.Equals( b ) )
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as SignalsToActions<T> );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                foreach ( SignalToAction<T> sta in _signalToActions )
                {
                    hash = hash * 31 + sta.GetHashCode();
                }
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{string.Join( ' ', _signalToActions )}";
        }
    }
}