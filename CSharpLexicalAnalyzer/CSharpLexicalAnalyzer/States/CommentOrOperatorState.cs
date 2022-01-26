using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class CommentOrOperatorState : AbstractState
{
    public CommentOrOperatorState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Identifier );
    }

    protected override void OnDigit()
    {
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Number );
    }

    protected override void OnDelimeter()
    {
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Delimeter );
    }

    protected override void OnOperator()
    {
        if ( _currentSymbol == "*" )
        {
            _automate.AppendToBuffer( _currentSymbol );
            _automate.SetState( LexerState.MultiLineComment );
            return;
        }

        TokenInfo tokenInfo = new( TokenType.Operator, _automate.Position, _automate.Buffer, _automate.LineNumber );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Operator );
    }

    protected override void OnDoubleQuote()
    {
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.StringLiteral );
    }

    protected override void OnQuote()
    {
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Character );
    }

    protected override void OnEndOfLine()
    {
        base.OnEndOfLine();
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
        _automate.SetState( LexerState.SingleLineComment );
        _automate.AppendToBuffer( _currentSymbol );
    }
}
