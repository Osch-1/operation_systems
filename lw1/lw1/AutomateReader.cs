namespace lw1
{
    public class AutomateReader
    {
        private readonly StreamReader _streamReader;

        public AutomateReader( StreamReader streamReader )
        {
            _streamReader = streamReader;
        }

        /// <summary>
        /// Returns automate which was readed from stream
        /// </summary>        
        public Automate GetAutomate()
        {
            AutomateType type = ReadAutomateType();
            List<InputToTransitions> inputsToTransitions = ReadInputsToTransitions();
            Automate automate = new( type, inputsToTransitions );

            return automate;
        }

        private AutomateType ReadAutomateType()
        {
            string? automateTypeStr = _streamReader.ReadLine();
            AutomateType automateType = automateTypeStr.To<AutomateType>();

            if ( automateType == AutomateType.Unknown )
            {
                throw new ArgumentException( nameof( automateType ) );
            }

            return automateType;
        }


        private List<InputToTransitions> ReadInputsToTransitions()
        {
            List<string>? states = _streamReader?.ReadLine()?.Split( ' ' )?.ToList();
            if ( states is null || !states.Any() )
            {
                throw new ArgumentException( "File doesnt contain states" );
            }

            List<InputToTransitions> result = new();

            List<string> lines = _streamReader.ReadToEnd().Split( Environment.NewLine ).ToList();
            foreach ( string line in lines )
            {
                List<string>? inputToActions = line.Split( ' ' ).ToList();
                string input = inputToActions[ 0 ][ ..^1 ];

                List<string> actions = inputToActions.Skip( 1 ).ToList();

                InputToTransitions inputToTransitions = new( input );

                foreach ( var ( action, index ) in actions.WithIndex() )
                {
                    Transition transition = new( states[ index ], action );
                    inputToTransitions.AddTransition( transition );
                }

                result.Add( inputToTransitions );
            }

            return result;
        }
    }
}
