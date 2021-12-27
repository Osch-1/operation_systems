namespace minimizer.Automate.Mealy
{
    public class SignalsToActions
    {
        private readonly HashSet<SignalToAction> _signalToActions = new();

        public IReadOnlyList<SignalToAction> SignalToActions => _signalToActions.ToList();

        public SignalsToActions( IEnumerable<SignalToAction> signalToActions )
        {
            _signalToActions = signalToActions.ToHashSet();
        }
    }
}