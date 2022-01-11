namespace CSharpLexicalAnalyzer
{
    public interface ILexerState
    {
        //returns last none whitespace(\r \t \b...)  character
        public string GetTokenizationInfo();
    }
}
