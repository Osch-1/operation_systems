using System.Diagnostics.CodeAnalysis;
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

        public override void Minimize()
        {
            var states = States.ToList();

            var s = states.GroupBy( s => s.SignalsToActions );
        }

        public override string ToString()
        {
            StringBuilder result = new( 300 );

            string[] stateNames = States.Select( s => s.Name ).ToArray();
            result.AppendLine( string.Join( ' ', stateNames ) );

            List<string> signals = States.SelectMany( s => s.SignalsToActions.SignalToActions ).Select( sta => sta.Signal.Name ).Distinct().ToList();
            for ( int i = 0; i < signals.Count; i++ )
            {
                var signal = signals[ i ];
                List<MealyAction> actions = States.SelectMany( s => s.SignalsToActions.SignalToActions ).Where( sta => sta.Signal.Name.Equals( signal ) ).Select( sta => sta.Action ).ToList();

                result.Append( $"{signal}: " );
                result.AppendLine( string.Join( ' ', actions ) );
            }

            return result.ToString();
        }

        private class EqualityClass
        {

        }
    }
}
