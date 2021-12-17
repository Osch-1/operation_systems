
// See https://aka.ms/new-console-template for more information

using lw1;

public class InputToTransitions
{
    private string _input;
    private List<Transition> _transitions;

    public string Input => _input;
    public List<Transition> Transitions => _transitions ??= new();

    public InputToTransitions( string input )
    {
        _input = input;
    }

    public void AddTransition( Transition transition )
    {
        Transitions.Add( transition );
    }

    public IReadOnlyList<string> GetStates()
    {
        return Transitions.Select( x => x.CurrentState ).ToList();
    }

    public IReadOnlyList<string> GetActions()
    {
        return Transitions.Select( x => x.Action ).ToList();
    }
}
