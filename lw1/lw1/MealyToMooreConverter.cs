using lw1;

public class MealyToMooreConverter
{
    private const string _statePrefix = "z";
    public static Automate Convert( Automate automate )
    {
        if ( automate.AutomateType == lw1.AutomateType.Moore )
        {
            return automate;//it's better to add copy method, but i d c
        }

        if ( automate.AutomateType != lw1.AutomateType.Mealy )
        {
            throw new ArgumentException();
        }

        int inputsCount = automate.InputsToTransitions.Count;
        List<string> inputs = automate.GetInputs().ToList();
        HashSet<string> mealyActions = automate.GetActions().OrderBy( a => a ).ToHashSet();
        List<InputToTransitions> inputsToTransitionsInMooreFormat = new( inputsCount );

        Dictionary<string, string> mooreStateByMealyAction = new();
        foreach ( (string mealyAction, int index) in mealyActions.WithIndex() )
        {
            mooreStateByMealyAction.Add( mealyAction, $"{_statePrefix}{index}" );
        }

        foreach ( var input in inputs )
        {
            inputsToTransitionsInMooreFormat.Add( new InputToTransitions( input ) );
        }

        foreach ( (string mealyAction, int index) in mealyActions.WithIndex() )
        {
            string mealyState = mealyAction.Split( '/' )[ 0 ];
            List<Transition> mealyTransitionsByState = automate.InputsToTransitions.SelectMany( it => it.Transitions ).Where( t => t.CurrentState == mealyState ).ToList();

            var mooreSrcState = mooreStateByMealyAction[ mealyAction ];
            foreach ( (Transition transition, int i) in mealyTransitionsByState.WithIndex() )
            {
                var mooreAction = mooreStateByMealyAction[ transition.Action ];
                Transition mooreTransition = new( $"{mooreSrcState}/{mealyAction.Split( '/' )[ 1 ]}", mooreAction );
                inputsToTransitionsInMooreFormat[ i ].AddTransition( mooreTransition );
            }
        }

        return new Automate( AutomateType.Moore, inputsToTransitionsInMooreFormat );
    }


}