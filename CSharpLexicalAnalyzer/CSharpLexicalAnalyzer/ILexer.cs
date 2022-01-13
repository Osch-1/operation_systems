// See https://aka.ms/new-console-template for more information
namespace CSharpLexicalAnalyzer
{
    public interface ILexer
    {
        public List<TokenInfo> TokenizeAsync( StreamReader reader );
    }
}