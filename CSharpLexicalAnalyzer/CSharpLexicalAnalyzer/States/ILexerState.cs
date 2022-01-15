namespace CSharpLexicalAnalyzer.States;

internal interface ILexerState
{
    //a way to provide input signal for current state
    public void Next( char character );
}
