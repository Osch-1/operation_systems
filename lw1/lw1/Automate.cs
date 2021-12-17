// See https://aka.ms/new-console-template for more information

using System.Text;
using lw1;

public class Automate
{
    private AutomateType _type;
    private List<InputToTransitions> _inputsToTransitions;

    public AutomateType AutomateType => _type;
    public IReadOnlyList<InputToTransitions> InputsToTransitions => _inputsToTransitions;

    public Automate( AutomateType type )
    {
        _type = type;
    }

    public void SetInputsToTransitions( IEnumerable<InputToTransitions> inputsToTransitions )
    {
        if ( inputsToTransitions is null )
        {
            throw new ArgumentNullException( nameof( inputsToTransitions ) );
        }

        _inputsToTransitions = inputsToTransitions.ToList();
    }

    public override string ToString()
    {
        StringBuilder sb = new( 100 );
        sb.AppendLine( _type.ToString() );
        var states = GetStates();
        sb.AppendLine( string.Join( " ", states.ToArray() ) );

        foreach ( var inputToTransitions in _inputsToTransitions )
        {
            sb.Append( $"{inputToTransitions.Input}: " );
            var actions = inputToTransitions.GetActions();
            sb.AppendLine( string.Join( " ", actions.ToArray() ) );
        }

        return sb.ToString();
    }

    //for now just take first row and call SetStates
    //assume that each transition is defined
    private IReadOnlyList<string> GetStates()
    {
        return _inputsToTransitions.First().GetStates();
    }
}