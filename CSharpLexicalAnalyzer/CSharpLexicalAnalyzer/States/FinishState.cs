using CSharpLexicalAnalyzer.LexemReflection;

namespace CSharpLexicalAnalyzer.States;

internal class FinishState : AbstractState
{
    public FinishState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
        : base( automate, lexemTypeResolver )
    {
    }

    protected override void OnLetter()
    {
        
    }

    protected override void OnDigit()
    {
        
    }

    protected override void OnDelimeter()
    {
        
    }

    protected override void OnOperator()
    {
        
    }

    protected override void OnDoubleQuote()
    {
        
    }

    protected override void OnQuote()
    {
        
    }

    protected override void OnEndOfLine()
    {
        
    }

    protected override void OnUnknown()
    {
        
    }

    protected override void OnSlash()
    {
        
    }
}
