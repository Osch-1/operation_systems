// See https://aka.ms/new-console-template for more information

using arithmetic_anal;

string? input = Console.ReadLine();

Abobus abobus = new( input );

try
{
    E( input );
    Console.WriteLine( "Success" );
}
catch ( Exception ex )
{
    Console.WriteLine( "Error" );
}

void E( string str )
{
    if ( str is null || !str.Any() )
    {
        throw new ArgumentException( nameof( str ) );
    }

    T( abobus.GetCurrent() );
    EShtrih( abobus.GetCurrent() );
}

void EShtrih( string str )
{
    if ( str != "+" )
    {
        abobus.MoveCarret();
        return;
    }

    T( abobus.GetCurrent() );
    EShtrih( abobus.GetCurrent() );
}

void T( string str )
{
    if ( str is null || !str.Any() )
    {
        throw new ArgumentException( nameof( str ) );
    }

    F( abobus.GetCurrent() );
    TShtrih( abobus.GetCurrent() );
}

void TShtrih( string str )
{
    if ( str != "*" )
    {
        abobus.MoveCarret();
        return;
    }

    T( abobus.GetCurrent() );
    EShtrih( abobus.GetCurrent() );
}

void F( string str )
{
    if ( str == "(" )
    {
        E( abobus.GetCurrent() );
    }

    if ( str == "-" || str == ")" || str == "7" || str == "a" )
    {
        abobus.MoveCarret();
        return;
    }

    throw new ArgumentException( nameof( str ) );
}

public class Abobus
{
    private string _str;
    private int _curr = 0;

    public Abobus( string str )
    {
        _str = str;
    }

    public string GetCurrent()
    {
        string res = _str[ _curr ].ToString();
        return res;
    }

    public void MoveCarret()
    {
        _curr++;
    }
}