using determination;

const char _delimeter = ' ';
const char _descriptionsDelimeter = '|';
const string Epsilon = "e";

Console.Write( "Type input file name: " );
string sourceFileName = Console.ReadLine();
Console.WriteLine();

Console.Write( "Type output file name: " );
string outputFileName = Console.ReadLine();
Console.WriteLine();

string path = Path.Combine( Environment.CurrentDirectory, $"{sourceFileName}.txt" );
StreamReader reader = new( path );

string outputFilePath = Path.Combine( Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.Parent.FullName, $"{outputFileName}.txt" );
StreamWriter _output = new( File.Open( outputFilePath, FileMode.Create ) )
{
    AutoFlush = true
};

Dictionary<string, List<TransitionInfo>> transitionInfosByState = new()
{
};

string[] grammarDescriptions = reader.ReadToEnd().Split( Environment.NewLine );
foreach ( string grammarDescription in grammarDescriptions )
{
    string[] descriptionTokens = grammarDescription.Split( _delimeter );
    string unit = descriptionTokens[ 0 ][ 0..^1 ].ToString();
    transitionInfosByState.Add( unit, new() );

    string[] transitions = descriptionTokens[ 1 ].Split( _descriptionsDelimeter );
    foreach ( string transition in transitions )
    {
        TransitionInfo transitionInfo = TransitionInfo.FromSymbolAndTransition( unit, transition );
        transitionInfosByState[ unit ].Add( transitionInfo );
    }
}

transitionInfosByState.Add( TransitionInfo._finalState, new List<TransitionInfo>() );

Dictionary<string, List<string>> achievableWithEpsilonByState = new();
Dictionary<string, List<TransitionInfo>> newTransitionsByState = new();

foreach ( (string stateName, List<TransitionInfo> transitions) in transitionInfosByState )
{
    achievableWithEpsilonByState[ stateName ] = new() { stateName };
    List<string> achievableByEpsilon = transitions.Where( ti => ti.Signal == Epsilon ).Select( ti => ti.To ).Distinct().ToList();
    achievableWithEpsilonByState[ stateName ].AddRange( achievableByEpsilon );

    List<string> tempAchievables = achievableByEpsilon.ToList();
    while ( tempAchievables.Any() )
    {
        List<string> newAchievables = new();
        foreach ( string tempAchievable in tempAchievables )
        {
            newAchievables.AddRange( transitionInfosByState[ tempAchievable ].Where<TransitionInfo>( ti => ti.Signal == Epsilon ).Select<TransitionInfo, string>( ti => ti.To ).Distinct<string>() );
        }

        tempAchievables = newAchievables.Distinct().ToList();
        achievableWithEpsilonByState[ stateName ].AddRange( tempAchievables );
    }


    HashSet<TransitionInfo> newTransitions = new();
    foreach ( string achievableStateName in achievableWithEpsilonByState[ stateName ] )
    {
        List<TransitionInfo> transitionsOfAchievableState = transitionInfosByState[ achievableStateName ].Where( ti => ti.Signal != Epsilon ).ToList();
        IEnumerable<TransitionInfo> b = transitionsOfAchievableState.Select( ti => new TransitionInfo { From = stateName, Signal = ti.Signal, To = ti.To } );
        newTransitions.UnionWith( b );
    }

    newTransitionsByState[ stateName ] = newTransitions.ToList();
}

foreach ( (string state, List<TransitionInfo> transitionInfos) in newTransitionsByState )
{
    if ( state != TransitionInfo._finalState )
    {
        string b = string.Join( "|", transitionInfos );
        await _output.WriteLineAsync( $"{state}: {b}" );
    }
}