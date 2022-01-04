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
            Console.WriteLine(automate);
        }
    }

    public class MealyStateEqualityComparer : IEqualityComparer<MealyState>
    {
        public bool Equals( MealyState x, MealyState y )
        {
            return x.SignalsToActions.ToString().Equals( y.SignalsToActions.ToString() );
        }

        public int GetHashCode( [DisallowNull] MealyState obj )
        {
            //GetHashCode - exclued names from states
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
    }
}