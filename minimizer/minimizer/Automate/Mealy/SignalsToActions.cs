namespace minimizer.Automate.Mealy
{
    public class SignalsToActions : IEquatable<SignalsToActions>
    {
        private readonly List<SignalToAction> _signalToActions = new();

        public IReadOnlyList<SignalToAction> SignalToActions => _signalToActions.ToList();

        public SignalsToActions( IEnumerable<SignalToAction> signalToActions )
        {
            _signalToActions = signalToActions.ToList();
        }

        public void AddSignalToAction( SignalToAction signalToAction )
        {
            if ( _signalToActions.Contains( signalToAction ) )
            {
                throw new ArgumentException( $"Current SignalsToActions instance already contains such SignalToAction pair{signalToAction}" );
            }

            _signalToActions.Add( signalToAction );
        }

        public bool Equals( SignalsToActions other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( other, this ) )
            {
                return true;
            }

            return _signalToActions.SequenceEqual( other._signalToActions );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as SignalsToActions );
        }

        public override int GetHashCode()
        {
            return _signalToActions.GetHashCode();
        }
    }
}