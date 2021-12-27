namespace minimizer.Mealy
{
    public class Action : IEquatable<Action>
    {
        /// <summary>
        /// State that automate will be in on t time
        /// </summary>
        private readonly State _state;
        /// <summary>
        /// Output that automate will produce on t time
        /// </summary>
        private readonly Output _output;

        public State State => _state;
        public Output Output => _output;

        public Action( State state, Output output )
        {
            _state = state;
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

            return other._state.Equals( _state )
                && other._output.Equals( _output );
        }

        public override bool Equals( object obj )
        {
            return Equals( obj as Action );
        }

        public override int GetHashCode()
        {
            return HashCode.Combine( _state, _output );
        }

        public override string ToString()
        {
            return $"{_state}/{_output}";
        }
    }
}
