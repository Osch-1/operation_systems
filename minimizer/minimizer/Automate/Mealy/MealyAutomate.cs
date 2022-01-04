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
            List<IGrouping<SignalsToActions<MealyAction>, MealyState>> statesByStaGroup = states.GroupBy( s => s.SignalsToActions ).ToList();
            List<EqualityClass> equalityClasses = statesByStaGroup.Select( s => new EqualityClass( s ) ).ToList();
            foreach ( EqualityClass equalityClass in equalityClasses )
            {
                foreach ( StateToEqualityClass stec in equalityClass.StatesToEqualityClass )
                {
                    MealyState state = stec.State;

                    foreach ( SignalToAction<MealyAction> signalToActions in state.SignalsToActions.SignalToActions )
                    {
                        MealyState actionState = signalToActions.Action.State;
                        EqualityClass eqClassByActionState = equalityClasses.Find( ec => ec.StatesToEqualityClass.Exists( stc => stc.State.Equals( actionState ) ) );
                        stec.EqualityClasses.Add( eqClassByActionState );
                    }
                }
            }
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

        private class StateToEqualityClass
        {
            public MealyState State { get; set; }
            public List<EqualityClass> EqualityClasses { get; set; } = new();

            public StateToEqualityClass( MealyState state )
            {
                State = state;
            }


        }

        private class EqualityClass
        {
            private static int counter = 0;

            public string Name { get; set; }
            public List<StateToEqualityClass> StatesToEqualityClass { get; set; }

            public EqualityClass( IEnumerable<MealyState> states )
            {
                Name = $"A{counter}";
                StatesToEqualityClass = states.Select( s => new StateToEqualityClass( s ) ).ToList();
                counter++;
            }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
