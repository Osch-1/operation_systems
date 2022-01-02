using minimizer.Automate;
using minimizer.Automate.Mealy;
using minimizer.Automate.Moore;
using minimizer.Extensions;

namespace minimizer
{
    public class AutomateReader
    {
        private const char _actionTokensDelimeter = '/';
        private const char _commonDelimeter = ' ';

        private readonly StreamReader _streamReader;

        public AutomateReader( StreamReader streamReader )
        {
            _streamReader = streamReader;
        }

        public IAutomate ReadFromStream()
        {
            AutomateType type = ReadAutomateType();

            return type switch
            {
                AutomateType.Mealy => ReadMealyAutomate(),
                AutomateType.Moore => ReadMooreAutomate(),
                _ => throw new ArgumentException( "Provided stream contains unsupported automate type" )
            };
        }

        private AutomateType ReadAutomateType()
        {
            string automateTypeStr = _streamReader.ReadLine();
            AutomateType automateType = automateTypeStr.To<AutomateType>();

            if ( automateType == AutomateType.Unknown )
            {
                throw new ArgumentException( nameof( automateType ) );
            }

            return automateType;
        }

        private IAutomate ReadMealyAutomate()
        {
            List<MealyState> states = _streamReader.ReadLine()
                .Split( ' ' )
                .ToList()
                .Select( str => new MealyState( str ) )
                .ToList();

            var lines = _streamReader.ReadToEnd().Split( Environment.NewLine );

            foreach ( string line in lines )
            {
                string[] tokens = line.Split( ' ' );
                Signal signal = new( tokens[ 0 ][ 0..^1 ] );

                string[] actions = tokens[ 1.. ];
                foreach ( (string action, int index) in actions.Select( ( action, index ) => (action, index) ) )
                {
                    string[] actionTokens = action.Split( _actionTokensDelimeter );

                    string destStateName = actionTokens[ 0 ];
                    string outputName = actionTokens[ 1 ];

                    MealyState destState = states.FirstOrDefault( s => s.Name == destStateName );
                    if ( destState is null )
                    {
                        throw new ArgumentException( $"Incorrect input format, unexpected state \"{destStateName}\" in action {destStateName}." );
                    }

                    Output output = new( outputName );
                    SignalToAction<MealyAction> signalToAction = new( signal, new( destState, output ) );

                    states[ index ].SignalsToActions.AddSignalToAction( signalToAction );
                }
            }

            return new MealyAutomate( states );
        }

        private IAutomate ReadMooreAutomate()
        {
            List<MooreState> states = _streamReader.ReadLine()
                .Split( ' ' )
                .ToList()
                .Select( str =>
                {
                    var tokens = str.Split( '/' );
                    string name = tokens[ 0 ];
                    string outputName = tokens[ 1 ];
                    return new MooreState( name, new Output( outputName ) );
                } )
                .ToList();

            var lines = _streamReader.ReadToEnd().Split( Environment.NewLine );

            foreach ( string line in lines )
            {
                string[] tokens = line.Split( ' ' );
                Signal signal = new( tokens[ 0 ][ 0..^1 ] );

                string[] actions = tokens[ 1.. ];
                foreach ( (string action, int index) in actions.Select( ( action, index ) => (action, index) ) )
                {
                    string destStateName = action;
                    MooreState destState = states.FirstOrDefault( s => s.Name == destStateName );
                    if ( destState is null )
                    {
                        throw new ArgumentException( $"Incorrect input format, unexpected state \"{destStateName}\" in action {destStateName}." );
                    }

                    SignalToAction<MooreAction> signalToAction = new( signal, new MooreAction( destState ) );

                    states[ index ].SignalsToActions.AddSignalToAction( signalToAction );
                }
            }

            return new MooreAutomate( states );
        }
    }
}
