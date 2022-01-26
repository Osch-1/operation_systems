using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class EmptyBufferState : AbstractState
{
    public EmptyBufferState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        _automate.SetState( LexerState.Identifier );
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDigit()
    {
        _automate.SetState( LexerState.Number );
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDelimeter()
    {
        _automate.SetState( LexerState.Delimeter );
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnOperator()
    {
        _automate.SetState( LexerState.Operator );
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnDoubleQuote()
    {
        _automate.SetState( LexerState.StringLiteral );
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnQuote()
    {
        _automate.SetState( LexerState.Character );
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnEndOfLine()
    {
        base.OnEndOfLine();
        string val = _currentSymbol == "\n" ? "\\n" : "\\r\\n";
        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.EndOfLine, _automate.Position, val, _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
    }

    protected override void OnUnknown()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnSlash()
    {
        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.CommentOrOperator );
    }
}
