using System.Text;

namespace minimizer.Mealy
{
    public class Automate
    {
        private Dictionary<State, HashSet<SignalToAction>> _tabledAutomate = new();

        public void AddState( State state )
        {
            if ( _tabledAutomate.ContainsKey( state ) )
            {
                throw new InvalidOperationException( $"Automate already contains such state: {state}" );
            }

            _tabledAutomate.Add( state, new() );
        }

        public void AddStates( IEnumerable<State> states )
        {
            foreach ( State state in states )
            {
                AddState( state );
            }
        }

        public void AddSignalToAction( State state, SignalToAction signalToAction )
        {
            if ( !_tabledAutomate.ContainsKey( state ) )
            {
                AddState( state );
                _tabledAutomate[ state ] = new HashSet<SignalToAction>
                {
                    signalToAction
                };
            }
            else
            {
                if ( _tabledAutomate[ state ].Contains( signalToAction ) )
                {
                    throw new InvalidOperationException( $"Automate already contains such state -> (signal -> action) pair: {state}->{signalToAction}" );
                }
                _tabledAutomate[ state ].Add( signalToAction );
            }
        }

        public static Automate FromStream( StreamReader stream )
        {
            string type = stream.ReadLine();
            if ( type != "Mealy" )
            {
                throw new ArgumentException( "Expected Moore automate but Mealy has been found." );
            }

            Automate automate = new();

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
        }
    }
}
