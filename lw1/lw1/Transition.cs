// See https://aka.ms/new-console-template for more information

namespace lw1
{
    public class Transition
    {
        private readonly string _currentState;
        private readonly string _action;

        public string CurrentState => _currentState;
        public string Action => _action;

        public Transition( string currentState, string action )
        {
            _currentState = currentState ?? throw new ArgumentNullException( nameof( currentState ) );
            _action = action ?? throw new ArgumentNullException( nameof( action ) );
        }
    }
}