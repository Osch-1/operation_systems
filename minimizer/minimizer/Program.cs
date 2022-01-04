using System.Diagnostics.CodeAnalysis;
using minimizer.Automate;
using minimizer.Automate.Mealy;

namespace minimizer
{
    public class Program
    {
        public static void Main( string[] args )
        {
            string path = Path.Combine( Environment.CurrentDirectory, "mealy.txt" );
            AutomateReader reader = new( new StreamReader( path ) );
            IAutomate automate = reader.ReadFromStream();

            automate.Minimize();

            var a = automate as MealyAutomate;

            Console.WriteLine( a.States[ 1 ].Equals( a.States[ 2 ] ) );

            var b = a.States.Distinct().ToList();

            Console.WriteLine( b.Count );

            IState st = a.States[ 1 ];
            IState st2 = a.States[ 2 ];
            Console.WriteLine( st.Equals( st2 ) );
        }
    }

    /*    public class MealyStateEqualityComparer : IEqualityComparer<MealyState>
        {
            public bool Equals( MealyState x, MealyState y )
            {
                return x.Equals( y );
            }

            public int GetHashCode( [DisallowNull] MealyState obj )
            {
                return obj.GetHashCode();
            }
        }

        public class SignalsToActionsEqualityComparer : IEqualityComparer<SignalsToActions<MealyAction>>
        {
            public bool Equals( SignalsToActions<MealyAction> x, SignalsToActions<MealyAction> y )
            {
                return x.Equals( y );
            }

            public int GetHashCode( [DisallowNull] SignalsToActions<MealyAction> obj )
            {
                return obj.GetHashCode();
            }
        }*/
}