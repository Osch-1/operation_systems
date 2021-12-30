using minimizer.Automate;
using minimizer.Automate.Mealy;
using minimizer.Extensions;

namespace minimizer
{
    public class AutomateReader
    {
        private const char ActionTokensDelimeter = '/';
        private const char CommonDelimeter = ' ';

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
                    string[] actionTokens = action.Split( ActionTokensDelimeter );

                    string destStateName = actionTokens[ 0 ];
                    string output = actionTokens[ 1 ];

                    var destStateByName = states.FirstOrDefault( s => s.Name == destStateName );
                    if ( destStateByName is null )
                    {
                        throw new ArgumentException( $"Incorrect input format, unexpected state in action {destStateName}." );
                    }

                }
            }


            return new MealyAutomate();
        }

        private IAutomate ReadMooreAutomate()
        {
            throw new NotImplementedException();
        }
    }
}
