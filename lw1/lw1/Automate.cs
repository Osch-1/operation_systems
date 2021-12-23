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

    public Automate( AutomateType type, IEnumerable<InputToTransitions> inputsToTransitions )
    {
        if ( inputsToTransitions is null )
        {
            throw new ArgumentNullException( nameof( inputsToTransitions ) );
        }

        _type = type;
        _inputsToTransitions = inputsToTransitions.ToList();
    }

    public Automate ToType( AutomateType type )
    {
        return type switch
        {
            AutomateType.Mealy => MooreToMealyConverter.Convert( this ),
            AutomateType.Moore => MealyToMooreConverter.Convert( this ),
            AutomateType.Unknown => throw new ArgumentException( $"Can't convert automate with {AutomateType.Unknown} type between types" ),
            _ => throw new ArgumentException( "Unknown automate type" ),
        };
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
    public IReadOnlyList<string> GetStates()
    {
        return _inputsToTransitions.First().GetStates();
    }

    public IReadOnlyList<string> GetActions()
    {
        return _inputsToTransitions.SelectMany( it => it.Transitions ).Select( t => t.Action ).ToList();
    }

    public IReadOnlyList<string> GetInputs()
    {
        return _inputsToTransitions.Select( it => it.Input ).ToList();
    }
}