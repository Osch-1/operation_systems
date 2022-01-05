using System.Text;

namespace minimizer.Automate
{
    public abstract class Automate<T> : IAutomate
        where T : IState
    {
        private List<T> _states;

        public IReadOnlyList<T> States => _states;

        public Automate()
        {
            _states = new();
        }

        public Automate( IEnumerable<T> states )
        {
            _states = states.ToList();
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

        protected void SetStates( IEnumerable<T> newStates )
        {
            _states = newStates.ToList();
        }

        public abstract void Minimize();
    }
}
