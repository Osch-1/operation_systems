using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class IdentifierState : AbstractState
{
    public IdentifierState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDigit()
    {
        _automate.AppendToBuffer( _currentSymbol );
        if ( _automate.Buffer.Length == 1 )
        {
            _automate.SetState( LexerState.Number );
        }
    }

    protected override void OnDelimeter()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Delimeter );
    }

    protected override void OnOperator()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Operator );
    }

    protected override void OnDoubleQuote()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.StringLiteral );
    }

    protected override void OnQuote()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Character );
    }

    protected override void OnEndOfLine()
    {
        TokenInfo tokenInfo = new( TokenType.Identifier, _automate.Position, _automate.Buffer );
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
}