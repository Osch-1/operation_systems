namespace LLParser;

public class LLParser
{
    private readonly LLRules _rules;

    private int _currentRuleId;
    private int _currentCharIndex;
    private Stack<int> _rulesStack = new();

    public LLParser( ILLRulesReader reader )
    {
        _rules = reader.Read();

        if ( _rules.Rules.Count == 0 )
        {
            throw new Exception();
        }
        else
        {
            _currentRuleId = _rules.GetFirstRuleId();
        }
    }

    public bool IsMatch( string input )
    {
        if ( input is null )
        {
            return false;
        }

        InitializeState();
        LLRule currentRule = _rules.GetRuleByid( _currentRuleId );

        while ( true )
        {
            string? currentChar = _currentCharIndex >= 0 ? input[ _currentCharIndex ].ToString() : null;
            bool isCharMatchRule = currentRule.GuideSymbols.Contains( currentChar ) || ( currentRule.IsAcceptNull && currentChar == null );
            if ( isCharMatchRule )
            {
                if ( currentRule.IsEnd )
                {
                    return true;
                }
            }
            else if ( currentRule.ThrowIfNotMatch )
            {
                return false;
            }

            if ( currentRule.Shift )
            {
                if ( _currentCharIndex == input.Length - 1 || currentChar is null )
                {
                    _currentCharIndex = -10;
                }
                else
                {
                    _currentCharIndex++;
                }
            }

            if ( currentRule.StackNextRule )
            {
                _rulesStack.Push( ++_currentRuleId );
            }

            if ( isCharMatchRule )
            {
                if ( currentRule.NextRuleId == null )
                {
                    _currentRuleId = _rulesStack.Pop();
                }
                else
                {
                    _currentRuleId = currentRule.NextRuleId.Value;
                }
            }
            else if ( !currentRule.ThrowIfNotMatch )
            {
                _currentRuleId++;
            }

            currentRule = _rules.GetRuleByid( _currentRuleId );
        }
    }

    private void InitializeState()
    {
        _currentRuleId = _rules.GetFirstRuleId();
        _currentCharIndex = 0;
    }
}
