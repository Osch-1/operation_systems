using minimizer;
using minimizer.Automate;
using minimizer.Automate.Mealy;

public class Program
{
    public static void Main( string[] args )
    {
        string path = Path.Combine( Environment.CurrentDirectory, "mealy.txt" );
        AutomateReader reader = new( new StreamReader( path ) );
        IAutomate automate = reader.ReadFromStream();

        automate.Minimize();

        var a = automate as MealyAutomate;

        var s1 = a.States[ 1 ].SignalsToActions;
        var s2 = a.States[ 2 ].SignalsToActions;

        Console.WriteLine( s1.Equals( s2 ) );
    }
}