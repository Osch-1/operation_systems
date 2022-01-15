using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class OperatorState : AbstractState
{
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
        "??",
        "??",
        "::",
        "++",
        "--",
        "&&",
        "||",
        "->",
        "==",
        "!=",
        "<=",
        ">=",
        "+=",
        "-=",
        "*=",
        "/=",
        "%=",
        "&=",
        "|=",
        "^=",
        "<<",
        "<<=",
        "=>",
        ">>",
        ">>="
    };

    public OperatorState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        _automate.StoreTokenInfo( new( TokenType.Operator, _automate.Position, _automate.Buffer ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Identifier );
    }

    protected override void OnDigit()
    {
        _automate.StoreTokenInfo( new( TokenType.Operator, _automate.Position, _automate.Buffer ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Number );
    }

    protected override void OnDelimeter()
    {
        _automate.StoreTokenInfo( new( TokenType.Operator, _automate.Position, _automate.Buffer ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Delimeter );
    }

    protected override void OnOperator()
    {
        _automate.AppendToBuffer( _currentSymbol );
        if ( !IsValid() )
        {
            _automate.StoreTokenInfo( new( TokenType.Unknown, _automate.Position, _automate.Buffer ) );
            _automate.MoveCurrentPos();
            _automate.ClearBuffer();
            _automate.SetState( LexerState.EmptyBuffer );
        }
    }

    protected override void OnDoubleQuote()
    {
        _automate.StoreTokenInfo( new( TokenType.Operator, _automate.Position, _automate.Buffer ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.StringLiteral );
    }

    protected override void OnQuote()
    {
        _automate.StoreTokenInfo( new( TokenType.Operator, _automate.Position, _automate.Buffer ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Character );
    }

    protected override void OnEndOfLine()
    {
        TokenInfo tokenInfo = new( TokenType.Operator, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        string val = _currentSymbol == "\n" ? "\\n" : "\\r\\n";
        _automate.AppendToBuffer( val );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.EndOfLine, _automate.Position, val ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnUnknown()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.Unknown, _automate.Position, _currentSymbol ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnSlash()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.CommentOrOperator );
    }

    private bool IsValid()
    {
        if ( _operators.Contains( _automate.Buffer ) )
        {
            return true;
        }

        return false;
    }
}