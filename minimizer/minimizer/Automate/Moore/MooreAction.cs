namespace minimizer.Automate.Moore
{
    public class MooreAction : IAction, IEquatable<MooreAction>
    {
        private readonly MooreState _destState;

        public MooreState DestState => _destState;

        public MooreAction( MooreState destState )
        {
            _destState = destState;
        }

        public bool Equals( MooreAction other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( other, this ) )
            {
                return true;
            }

            return _destState.Name.Equals( other._destState.Name )
                && _destState.Output.Equals( other._destState.Output );
        }

        public bool Equals( IAction other )
        {
            return Equals( other as MooreAction );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as MooreAction );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _destState );
        }

        public override string ToString()
        {
            return $"{_destState.Name}";
        }
    }
}