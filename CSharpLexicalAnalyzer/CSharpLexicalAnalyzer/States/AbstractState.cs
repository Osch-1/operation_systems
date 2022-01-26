using Common.Types;
using CSharpLexicalAnalyzer.LexemReflection;

namespace CSharpLexicalAnalyzer.States;

internal abstract class AbstractState : ILexerState
{
    private readonly LexemTypeResolver _lexemTypeResolver;

    protected readonly ILexerAutomate _automate;
    protected string _currentSymbol = string.Empty;

    public AbstractState( ILexerAutomate automate, LexemTypeResolver lexemTypeResolver )
    {
        _automate = automate;
        _lexemTypeResolver = lexemTypeResolver;
    }

    public void Next( char character )
    {
        _currentSymbol = character.ToString();

        LexemType lexemType = _lexemTypeResolver.Resolve( _currentSymbol );

        switch ( lexemType )
        {
            case LexemType.Letter:
            case LexemType.Underline:
                OnLetter();
                break;
            case LexemType.Digit:
                OnDigit();
                break;
            case LexemType.Delimeter:
                OnDelimeter();
                break;
            case LexemType.Operator:
                OnOperator();
                break;
            case LexemType.DoubleQuote:
                OnDoubleQuote();
                break;
            case LexemType.Quote:
                OnQuote();
                break;
            case LexemType.EndOfLine:
                OnEndOfLine();
                break;
            case LexemType.Unknown:
                OnUnknown();
                break;
            case LexemType.Slash:
                OnSlash();
                break;
            default:
                break;
        }
    }

    protected abstract void OnLetter();
    protected abstract void OnDigit();
    protected abstract void OnDelimeter();
    protected abstract void OnOperator();
    protected abstract void OnDoubleQuote();
    protected abstract void OnQuote();
    protected virtual void OnEndOfLine()
    {
        _automate.IncrementLinesCounter();
    }
    protected abstract void OnUnknown();
    protected abstract void OnSlash();
}
