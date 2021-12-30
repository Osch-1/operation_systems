using System.Text;

namespace minimizer.Automate.Mealy
{
    public class MealyAutomate : Automate<MealyState>
    {
        public MealyAutomate()
            : base()
        {
        }

        public MealyAutomate( IEnumerable<MealyState> states )
            : base( states )
        {
        }

        public override string ToString()
        {
            StringBuilder result = new( 300 );

            string[] states = States.Select( x => x.Name ).ToArray();
            result.AppendLine( string.Join( ' ', states ) );

            List<string> signals = States.SelectMany( x => x.SignalsToActions.SignalToActions ).Select( sta => sta.Signal.Name ).Distinct().ToList();
            for ( int i = 0; i < signals.Count; i++ )
            {
                var signal = signals[ i ];
                List<Action> actions = States.SelectMany( x => x.SignalsToActions.SignalToActions ).Where( sta => sta.Signal.Name.Equals( signal ) ).Select( sta => sta.Action ).ToList();

                result.Append( $"{signal}: " );
                result.AppendLine( string.Join( ' ', actions ) );
            }

            return result.ToString();
        }
    }
}
