// See https://aka.ms/new-console-template for more information

using arithmetic_anal;

string? input = Console.ReadLine();
StringManager stringManager = new( input );
Console.WriteLine( AssertArithmetic() );

string AssertArithmetic()
{
    try
    {
        E();
        if ( stringManager.GetCurrentOrNull() != null )
        {
            throw new ArgumentException();
        }
        return "Success";
    }
    catch ( Exception )
    {
        return "Error";
    }
}

void E()
{
    if ( stringManager.GetCurrentOrNull() is null )
    {
        throw new ArgumentException();
    }

    T( stringManager.GetCurrentOrNull() );
    EShtrih( stringManager.GetCurrentOrNull() );
}

void T( string str )
{
    if ( str is null || !str.Any() )
    {
        throw new ArgumentException( nameof( str ) );
    }

    F( stringManager.GetCurrentOrNull() );
    TShtrih( stringManager.GetCurrentOrNull() );
}

void EShtrih( string str )
{
    if ( str is null )
    {
        return;
    }

    if ( str == "+" || str == "-" )
    {
        stringManager.MoveCarret();
        T( stringManager.GetCurrentOrNull() );
        EShtrih( stringManager.GetCurrentOrNull() );
    }
}

void TShtrih( string str )
{
    if ( str is null )
    {
        return;
    }

    if ( str == "*" )
    {
        stringManager.MoveCarret();
        T( stringManager.GetCurrentOrNull() );
        TShtrih( stringManager.GetCurrentOrNull() );
    }
}

void F( string str )
{
    if ( str == "-" )
    {
        stringManager.MoveCarret();
        F( stringManager.GetCurrentOrNull() );
        return;
    }

    if ( str == "(" )
    {
        stringManager.MoveCarret();
        E();

        if ( stringManager.GetCurrentOrNull() != ")" )
        {
            throw new ArgumentException();
        }

        stringManager.MoveCarret();
        return;
    }

    if ( str == "7" || str == "a" )
    {
        stringManager.MoveCarret();
        return;
    }

    throw new ArgumentException( nameof( str ) );
}

public class StringManager
{
    private string _str;
    private int _curr = 0;

    public StringManager( string str )
    {
        _str = str;
    }

    public string? GetCurrentOrNull()
    {
        try
        {
            string res = _str[ _curr ].ToString();
            return res;
        }
        catch ( IndexOutOfRangeException )
        {
            return null;
        }
    }

    public void MoveCarret()
    {
        _curr++;
    }
}