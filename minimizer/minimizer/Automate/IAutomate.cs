namespace minimizer.Automate
{
    public interface IAutomate<T> where T : IState
    {
        public IReadOnlyList<T> States
        {
            get;
        }

        public void AddState( T state );

        public void AddStates( IEnumerable<T> states );

        public Automate<T> FromStream( Stream stream );
    }
}