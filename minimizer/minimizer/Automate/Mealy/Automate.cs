using System.Text;

namespace minimizer.Automate.Mealy
{
    public class MealyAutomate
    {        
        private readonly List<State> _states = new();

        public void AddState( State state )
        {            
            if (!_statesH.Contains(state))
            {                
                _states.Add(state);
            }
            else
            {
                throw new InvalidOperationException( $"State {state} already exists" );
            }
        }

        public void AddStates( IEnumerable<State> states )
        {
            foreach ( State state in states )
            {
                AddState( state );
            }
        }        

        public static MealyAutomate FromStream( StreamReader stream )
        {
            string type = stream.ReadLine();
            if ( type != "Mealy" )
            {
                throw new ArgumentException( "Expected Mealy automate but Moore has been found." );
            }

            MealyAutomate automate = new();

            List<State> states = stream.ReadLine().Split( ' ' ).Select( s => new State( s ) ).ToList();
            automate.AddStates( states );

            List<string> lines = stream.ReadToEnd().Split( Environment.NewLine ).ToList();
            foreach ( string line in lines )
            {
                List<string> signalToActions = line.Split( ' ' ).ToList();
                Signal signal = new( signalToActions[ 0 ][ ..^1 ] );
                List<string> actions = signalToActions.ToArray()[ 1.. ].ToList();

                foreach ( (string action, int index) in actions.Select( ( a, index ) => (a, index) ) )
                {
                    State state = states[ index ];
                    if ( state is null )
                    {
                        throw new Exception( $"Not enough actions for {signal}" );
                    }

                    var splitedAction = action.Split( '/' );
                    string destStateName = splitedAction[ 0 ];
                    string output = splitedAction[ 1 ];

                    SignalToAction signalToAction = new( signal, new Action( new State( destStateName ), new Output( output ) ) );
                    automate.AddSignalToAction( state, signalToAction );
                }
            }

            return automate;
        }

        public override string ToString()
        {
            StringBuilder sb = new( 300 );

            sb.AppendLine( "Mealy" );
            foreach ( var a in _tabledAutomate )
            {
                sb.AppendLine( $"{a.Key}: {string.Join( ' ', a.Value )}" );
            }

            return sb.ToString();
        }*/
    }
}
