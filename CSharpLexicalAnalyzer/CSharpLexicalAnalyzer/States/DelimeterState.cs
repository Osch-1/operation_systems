using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class DelimeterState : AbstractState
{
    public DelimeterState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        _automate.StoreTokenInfo( new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Identifier );
    }

    protected override void OnDigit()
    {
        _automate.StoreTokenInfo( new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Number );
    }

    protected override void OnDelimeter()
    {
        _automate.StoreTokenInfo( new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Delimeter );
    }

    protected override void OnOperator()
    {
        _automate.StoreTokenInfo( new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Operator );
    }

    protected override void OnDoubleQuote()
    {
        _automate.StoreTokenInfo( new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.StringLiteral );
    }

    protected override void OnQuote()
    {
        _automate.StoreTokenInfo( new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.Character );
    }

    protected override void OnEndOfLine()
    {
        base.OnEndOfLine();
        string buffer = _automate.Buffer == "\r" ? "\\r" : _automate.Buffer;
        TokenInfo tokenInfo = new( TokenType.Delimeter, _automate.Position, $"'{buffer}'", _automate.LineNumber );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();


        string val = _currentSymbol == "\n" ? "\\n" : "\\r\\n";
        _automate.AppendToBuffer( val );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.EndOfLine, _automate.Position, $"'{val}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnUnknown()
    {
        TokenInfo tokenInfo = new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( new TokenInfo( TokenType.Unknown, _automate.Position, $"'{_currentSymbol}'", _automate.LineNumber ) );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnSlash()
    {
        TokenInfo tokenInfo = new( TokenType.Delimeter, _automate.Position, $"'{_automate.Buffer}'", _automate.LineNumber );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();

        _automate.AppendToBuffer( _currentSymbol );
        _automate.SetState( LexerState.CommentOrOperator );
    }
}