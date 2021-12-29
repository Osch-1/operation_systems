namespace minimizer.Automate.Mealy
{
    public class Action : IEquatable<Action>
    {
        private readonly StateInfo _stateInfo;
        private readonly Output _output;

        public StateInfo StateInfo => _stateInfo;
        public Output Output => _output;

        public Action( StateInfo stateInfo, Output output )
        {
            _stateInfo = stateInfo;
            _output = output;
        }

        public bool Equals( Action other )
        {
            if ( other is null )
            {
                return false;
            }

            if ( ReferenceEquals( other, this ) )
            {
                return true;
            }

            return other._stateInfo.Equals( _stateInfo )
                && other._output.Equals( _output );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as Action );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _stateInfo, _output );
        }

        public override string ToString()
        {
            return $"{_stateInfo}/{_output}";
        }
    }
}
