using System.Text;

namespace minimizer.Automate.Moore
{
    public class MooreAutomate : Automate<MooreState>
    {
        public MooreAutomate()
            : base()
        {
        }

        public MooreAutomate( IEnumerable<MooreState> states )
            : base( states )
        {
        }

        public override void Minimize()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder result = new( 300 );

            string[] states = States.Select( s => s.ToString() ).ToArray();
            result.AppendLine( string.Join( ' ', states ) );

            List<string> signals = States.SelectMany( s => s.SignalsToActions.SignalToActions ).Select( sta => sta.Signal.Name ).Distinct().ToList();
            for ( int i = 0; i < signals.Count; i++ )
            {
                var signal = signals[ i ];
                List<MooreAction> actions = States.SelectMany( s => s.SignalsToActions.SignalToActions ).Where( sta => sta.Signal.Name.Equals( signal ) ).Select( sta => sta.Action ).ToList();

                result.Append( $"{signal}: " );
                result.AppendLine( string.Join( ' ', actions ) );
            }

            return result.ToString();
        }
    }
}
