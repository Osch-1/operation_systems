using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer;

public interface ILexer
{
    public IReadOnlyList<TokenInfo> TokenizeAsync();
}
