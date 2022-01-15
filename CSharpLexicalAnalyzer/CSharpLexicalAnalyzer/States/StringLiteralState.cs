using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer.States;

internal class StringLiteralState : AbstractState
{
    public StringLiteralState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        ValidateAndAppendCurrent();
    }

    protected override void OnDigit()
    {
        ValidateAndAppendCurrent();
    }

    protected override void OnDelimeter()
    {
        ValidateAndAppendCurrent();
    }

    protected override void OnOperator()
    {
        ValidateAndAppendCurrent();
    }

    protected override void OnDoubleQuote()
    {
        if ( _automate.Buffer[ 0 ] != '"' )
        {
            throw new Exception( $"Automate appeared to be in {nameof( CharacterState )} state when first symbol is not '" );
        }

        TokenInfo tokenInfo = new( TokenType.StringLiteral, _automate.Position, _automate.Buffer );

        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
        _automate.SetState( LexerState.EmptyBuffer );
    }

    protected override void OnQuote()
    {
        ValidateAndAppendCurrent();
    }

    protected override void OnEndOfLine()
    {
        if ( _automate.Buffer[ 0 ] != '"' )
        {
            throw new Exception( $"Automate appeared to be in {nameof( CharacterState )} state when first symbol is not '" );
        }

        TokenInfo tokenInfo = new( TokenType.StringLiteral, _automate.Position, _automate.Buffer, "End of line on unclosed string literal" );
        _automate.AppendToBuffer( _currentSymbol );
        _automate.StoreTokenInfo( tokenInfo );
        _automate.MoveCurrentPos();
        _automate.ClearBuffer();
    }

    protected override void OnUnknown()
    {
        ValidateAndAppendCurrent();
    }

    protected override void OnSlash()
    {
        ValidateAndAppendCurrent();
    }

    private void ValidateAndAppendCurrent()
    {
        if ( _automate.Buffer[ 0 ] != '"' )
        {
            throw new Exception( $"Automate appeared to be in {nameof( CharacterState )} state when first symbol is not '" );
        }

        _automate.AppendToBuffer( _currentSymbol );
    }
}