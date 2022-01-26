using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class MultiLineCommentState : AbstractState
{
    public MultiLineCommentState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
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
    }

    protected override void OnDelimeter()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnOperator()
    {
        if ( _currentSymbol == "/" && _automate.Buffer.Last() == '*' )
        {
            _automate.AppendToBuffer( _currentSymbol );
            _automate.MoveCurrentPos();
            _automate.ClearBuffer();
            _automate.SetState( LexerState.EmptyBuffer );
        }
        else
        {
            _automate.AppendToBuffer( _currentSymbol );
        }
    }

    protected override void OnDoubleQuote()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnQuote()
    {
        _automate.AppendToBuffer( _currentSymbol );
    }

    protected override void OnEndOfLine()
    {
        base.OnEndOfLine();
        _automate.AppendToBuffer( _currentSymbol );
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
        throw new NotImplementedException();
    }
}