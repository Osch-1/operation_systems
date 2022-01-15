using CSharpLexicalAnalyzer.States;

namespace CSharpLexicalAnalyzer.Token.Reflection;

internal class TokenTypeResolver
{
    public TokenType Resolve( string tokenValue, ILexerState lexerState )
    {
        return TokenType.Unknown;
    }
}
