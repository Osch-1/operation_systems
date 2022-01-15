using System.Text.RegularExpressions;

namespace CSharpLexicalAnalyzer.LexemReflection;

internal class LexemTypeResolver
{
    private static readonly HashSet<string> _delimeters = new()
    {
        " ",
        ".",
        ",",
        ":",
        ";",
        "(",
        ")",
        "[",
        "]",
        "{",
        "}",
        "\r"
    };

    private static readonly HashSet<string> _operators = new()
    {
        "-",
        "*",
        "+",
        "%",
        "&",
        "|",
        "^",
        "!",
        "~",
        "=",
        "<",
        ">",
        "?",
    };
    private static readonly Regex _letterRegex = new( "^[a-zA-ZА-Яа-я]{1}$" );
    private static readonly Regex _digitRegex = new( @"^\d$" );

    public LexemType Resolve( string lexem )
    {

        if ( _letterRegex.IsMatch( lexem ) )
        {
            return LexemType.Letter;
        }

        if ( _digitRegex.IsMatch( lexem ) )
        {
            return LexemType.Digit;
        }

        if ( _operators.Contains( lexem ) )
        {
            return LexemType.Operator;
        }

        if ( _delimeters.Contains( lexem ) )
        {
            return LexemType.Delimeter;
        }

        if ( lexem == "\"" )
        {
            return LexemType.DoubleQuote;
        }

        if ( lexem == "'" )
        {
            return LexemType.Quote;
        }

        if ( lexem == "/" )
        {
            return LexemType.Slash;
        }

        if ( lexem == "\n" || lexem == "\r\n" )
        {
            return LexemType.EndOfLine;
        }

        if (lexem == "_")
        {
            return LexemType.Underline;
        }

        return LexemType.Unknown;
    }
}
