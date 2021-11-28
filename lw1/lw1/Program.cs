// See https://aka.ms/new-console-template for more information

using lw1;

string? automateTypeStr = Console.ReadLine();

IAutomateConverter converter = null;
AutomateType automateType = automateTypeStr.To<AutomateType>();

Automate sourceAutomate = new();

if ( automateType == AutomateType.Unknown )
{
    throw new ArgumentException( nameof( automateType ) );
}

Automate result = converter.Convert( sourceAutomate );
Console.WriteLine( result );

public interface IAutomateConverter
{
    Automate Convert( Automate automate );
}


public class Automate
{
    public Automate()
    {

    }
}