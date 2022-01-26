using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer;

internal interface ILexerAutomate
{
    public string Buffer { get; }
    public int Position { get; }
    public int LineNumber { get; }

    public void ClearBuffer();
    public void AppendToBuffer( string str );
    public void MoveCurrentPos();
    public void IncrementLinesCounter();
    public void SetState( LexerState state );
    public void StoreTokenInfo( TokenInfo tokenInfo );
}
