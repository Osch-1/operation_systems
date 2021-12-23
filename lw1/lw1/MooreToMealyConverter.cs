using lw1;

public class MooreToMealyConverter
{
    public static Automate Convert( Automate automate )
    {
        if ( automate.AutomateType == lw1.AutomateType.Mealy )
        {
            return automate;//it's better to add copy method
        }

        if ( automate.AutomateType != lw1.AutomateType.Moore )
        {
            throw new ArgumentException();
        }

        int inputsCount = automate.InputsToTransitions.Count;
        List<InputToTransitions> inputsToTransitionsInMealyFormat = new( inputsCount );
        foreach ( InputToTransitions inputToTransitions in automate.InputsToTransitions )
        {
            InputToTransitions inputToTransitionsInMealyFormat = new( inputToTransitions.Input );
            foreach ( Transition transition in inputToTransitions.Transitions )
            {
                string state = transition.CurrentState.Split( '/' )[ 0 ];
                string action = automate.GetStates().Where( s => s.IndexOf( state ) != -1 ).FirstOrDefault();
                Transition transitionInMealyFormat = new( state, action );
                inputToTransitionsInMealyFormat.AddTransition( transitionInMealyFormat );
            }
            inputsToTransitionsInMealyFormat.Add( inputToTransitionsInMealyFormat );
        }

        return new Automate( AutomateType.Mealy, inputsToTransitionsInMealyFormat );
    }
}