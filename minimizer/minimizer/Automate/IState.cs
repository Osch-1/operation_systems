namespace minimizer.Automate
{
    public interface IState : IEquatable<IState>
    {
        public string Name { get; }
    }
}
