// See https://aka.ms/new-console-template for more information
namespace CSharpLexicalAnalyzer
{
    public interface ILexerAutomate
    {
        public string Buffer { get; }

        public void ClearBuffer();
        public void SetBuffer( string buffer );
        public void SetState( ILexerState state );
        public void AddTokenInfo( TokenInfo tokenInfo );
    }
}