// See https://aka.ms/new-console-template for more information

using lw1;

string? automateTypeStr = Console.ReadLine();

AutomateType automateType = automateTypeStr.To<AutomateType>();
if ( automateType == AutomateType.Unknown )
{
    throw new ArgumentException( nameof( automateType ) );
}

Automate sourceAutomate = new( automateType );

public class Transition
{

}