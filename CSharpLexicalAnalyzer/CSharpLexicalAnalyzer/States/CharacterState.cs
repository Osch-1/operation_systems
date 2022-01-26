using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class CharacterState : AbstractState
{
    public CharacterState( ILexerAutomate automate, LexemTypeResolver lexerTypeResolver )
        : base( automate, lexerTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDigit()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDelimeter()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnOperator()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDoubleQuote()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnQuote()
    {
        List<string> _errors = new();

        if ( _automate.Buffer.Length < 2 )
        {
            _errors.Add( "Empty symbol doesn't exist" );
        }

        if ( _automate.Buffer.Length > 2 )
        {
            _errors.Add( "Too long character literal" );
        }

        TokenInfo tokenInfo = new( TokenType.Character, _automate.Position, _automate.Buffer, _automate.LineNumber, string.Join( ' ', _errors ) );

        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnEndOfLine()
    {
        base.OnEndOfLine();
        List<string> _errors = new();

        if ( _automate.Buffer.Length < 2 )
        {
            _errors.Add( "Empty symbol doesn't exist" );
        }

        if ( _automate.Buffer.Length > 2 )
        {
            _errors.Add( "Too long character literal" );
        }

        TokenInfo tokenInfo = new( TokenType.Character, _automate.Position, _automate.Buffer, _automate.LineNumber, string.Join( Environment.NewLine, _errors ) );

        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        string val = _currentSymbol == "\n" ? "\\n" : "\\r\\n";
        _automate.AppendToBuffer( val );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.EndOfLine, _automate.Position, val, _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnUnknown()
    {
        List<string> _errors = new();

        _errors.Add( "Unclosed character" );

        TokenInfo tokenInfo = new( TokenType.Character, _automate.Position, _automate.Buffer, _automate.LineNumber, string.Join( Environment.NewLine, _errors ) );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.Unknown, _automate.Position, _currentSymbol, _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnSlash()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }
}
