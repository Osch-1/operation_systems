using lw1;
using System.Reflection;

string path = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), @"mealy.txt" );
AutomateReader reader = new( new StreamReader( path ) );
Automate automate = reader.GetAutomate();

switch ( automate.AutomateType )
{
    case AutomateType.Moore:
        Console.WriteLine( automate.ToType( AutomateType.Mealy ) );
        return;
    case AutomateType.Mealy:
        Console.WriteLine( automate.ToType( AutomateType.Moore ) );
        return;
    default:
        throw new ArgumentException( "Unknown type" );
}