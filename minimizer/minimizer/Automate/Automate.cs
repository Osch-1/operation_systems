namespace minimizer.Automate
{
    public abstract class Automate<T> : IAutomate<T> where T : IState
    {
        private readonly List<T> _states;

        public IReadOnlyList<T> States => _states;

        public Automate()
        {
            _states = new();
        }

        public void AddState( T state )
        {
            _states.Add( state );
        }

        public void AddStates( IEnumerable<T> states )
        {
            foreach ( T state in states )
            {
                AddState( state );
            }
        }

        public abstract Automate<T> FromStream( Stream stream );
    }
}
