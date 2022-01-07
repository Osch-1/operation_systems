using System.Text;
using determination;

const char _delimeter = ' ';
const char _descriptionsDelimeter = '|';

Console.Write( "Type input file name: " );
string sourceFileName = Console.ReadLine();
Console.WriteLine();


string path = Path.Combine( Environment.CurrentDirectory, $"{sourceFileName}.txt" );
StreamReader reader = new( path );
GrammarType grammarType = GrammarTypeFromStr( reader.ReadLine() );
Dictionary<string, List<TransitionInfo>> automateWithSourceSymbols = GenerateAutomateForSourceStates();
FillTransitionsForCreatedStates();
PrintFinalAutomate();


void PrintFinalAutomate()
{
    List<string> signals = automateWithSourceSymbols.Values.SelectMany( s => s ).Select( ti => ti.Signal ).Distinct().ToList();
    List<string> states = automateWithSourceSymbols.Keys.ToList();

    Console.WriteLine( $"   {string.Join( ' ', states )}" );
    foreach ( string signal in signals )
    {
        StringBuilder sb = new( 30 );
        sb.Append( $"{signal}: " );

        List<string> outputs = new();
        foreach ( string state in states )
        {
            TransitionInfo transitionInfoBySignalAndState = automateWithSourceSymbols[ state ].Find( ti => ti.Signal == signal );
            outputs.Add( transitionInfoBySignalAndState is not null ? transitionInfoBySignalAndState.To : "#" );
        }

        sb.Append( string.Join( ' ', outputs ) );

        Console.WriteLine( sb );
    }
}

void FillTransitionsForCreatedStates()
{
    bool finish = false;
    while ( !finish )
    {
        HashSet<string> currentSymbols = automateWithSourceSymbols.Keys.ToHashSet();
        List<string> newStates = automateWithSourceSymbols.Values.SelectMany( v => v ).Select( ti => ti.To ).Distinct().Where( ti => !currentSymbols.Contains( ti ) ).ToList();
        if ( !newStates.Any() )
        {
            finish = true;
        }
        else
        {
            foreach ( string newState in newStates )
            {
                List<TransitionInfo> newStateTransitions = GenerateTransitionsForNewState( newState );
                automateWithSourceSymbols.Add( newState, newStateTransitions );
            }
        }
    }
}

List<TransitionInfo> GenerateTransitionsForNewState( string newState )
{
    List<string> sourceStatesInNewState = newState.Select( c => c.ToString() ).ToList();
    List<TransitionInfo> transitions = new();

    Dictionary<string, string> destStateBySignal = new();
    foreach ( string state in sourceStatesInNewState )
    {
        List<TransitionInfo> existingTransitions = automateWithSourceSymbols[ state ];
        foreach ( TransitionInfo existingTransition in existingTransitions )
        {
            if ( destStateBySignal.ContainsKey( existingTransition.Signal ) )
            {
                destStateBySignal[ existingTransition.Signal ] += existingTransition.To;
                destStateBySignal[ existingTransition.Signal ] = string.Concat( destStateBySignal[ existingTransition.Signal ].OrderBy( c => c ).Distinct() );
            }
            else
            {
                destStateBySignal.Add( existingTransition.Signal, existingTransition.To );
            }
        }
    }

    foreach ( (string signal, string destState) in destStateBySignal )
    {
        TransitionInfo transitionInfo = new()
        {
            From = newState,
            Signal = signal,
            To = destState
        };

        transitions.Add( transitionInfo );
    }

    return transitions;
}

Dictionary<string, List<TransitionInfo>> GenerateAutomateForSourceStates()
{
    if ( grammarType is GrammarType.Right )
    {
        return GenerateAutomateForSourceStatesByRightGrammar();
    }
    else if ( grammarType is GrammarType.Left )
    {
        return GenerateAutomateForSourceStatesByLeftGrammar();
    }

    throw new Exception( $"Unsupported grammar type {grammarType}" );
}

Dictionary<string, List<TransitionInfo>> GenerateAutomateForSourceStatesByRightGrammar()
{
    Dictionary<string, List<TransitionInfo>> transitionInfosBySymbol = new()
    {
        { "F", new List<TransitionInfo>() }
    };

    string[] grammarDescriptions = reader.ReadToEnd().Split( Environment.NewLine );
    foreach ( string grammarDescription in grammarDescriptions )
    {
        string[] descriptionTokens = grammarDescription.Split( _delimeter );
        string symbol = descriptionTokens[ 0 ][ 0..^1 ].ToString();
        transitionInfosBySymbol.Add( symbol, new() );

        string[] transitions = descriptionTokens[ 1 ].Split( _descriptionsDelimeter );
        foreach ( string transition in transitions )
        {
            TransitionInfo transitionInfo = TransitionInfo.FromSymbolAndRightTransition( symbol, transition );

            bool transitionExists = transitionInfosBySymbol[ symbol ].Any( ti => ti.Signal.Equals( transitionInfo.Signal ) );
            TransitionInfo transitionWithSuchSignal = transitionInfosBySymbol[ symbol ].Find( ti => ti.Signal.Equals( transitionInfo.Signal ) );
            if ( transitionWithSuchSignal is null )
            {
                transitionInfosBySymbol[ symbol ].Add( transitionInfo );
            }
            else
            {
                transitionWithSuchSignal.To += transitionInfo.To;
                transitionWithSuchSignal.To = string.Concat( transitionWithSuchSignal.To.OrderBy( c => c ).Distinct() );
            }
        }
    }

    return transitionInfosBySymbol;
}

Dictionary<string, List<TransitionInfo>> GenerateAutomateForSourceStatesByLeftGrammar()
{
    Dictionary<string, List<TransitionInfo>> transitionInfosBySymbol = new();

    string[] grammarDescriptions = reader.ReadToEnd().Split( Environment.NewLine );
    foreach ( string grammarDescription in grammarDescriptions )
    {
        string[] descriptionTokens = grammarDescription.Split( _delimeter );
        //due to left grammar it's a symbol 'to' in terms of this app
        string symbol = descriptionTokens[ 0 ][ 0..^1 ].ToString();

        string[] transitions = descriptionTokens[ 1 ].Split( _descriptionsDelimeter );
        foreach ( string transition in transitions )
        {
            TransitionInfo transitionInfo = TransitionInfo.FromSymbolAndLeftTransition( symbol, transition );

            if ( !transitionInfosBySymbol.ContainsKey( transitionInfo.From ) )
            {
                transitionInfosBySymbol.Add( transitionInfo.From, new List<TransitionInfo>() { transitionInfo } );
            }
            else
            {
                TransitionInfo transitionWithSuchSignal = transitionInfosBySymbol[ transitionInfo.From ].Find( ti => ti.Signal.Equals( transitionInfo.Signal ) );
                if ( transitionWithSuchSignal is null )
                {
                    transitionInfosBySymbol[ transitionInfo.From ].Add( transitionInfo );
                }
                else
                {
                    transitionWithSuchSignal.To += symbol;
                    transitionWithSuchSignal.To = string.Concat( transitionWithSuchSignal.To.OrderBy( c => c ).Distinct() );
                }
            }
        }
    }

    return transitionInfosBySymbol;
}

GrammarType GrammarTypeFromStr( string str )
{
    return str switch
    {
        "Right" => GrammarType.Right,
        "Left" => GrammarType.Left,
        _ => throw new Exception( $"Incorrect grammar type {str}" )
    };
}