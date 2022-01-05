using minimizer.Automate;

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
}