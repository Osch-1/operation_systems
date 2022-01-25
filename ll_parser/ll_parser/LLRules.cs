namespace LLParser;

public class LLRules
{
    private Dictionary<int, LLRule> _ruleById;

    public IReadOnlyList<LLRule> Rules => _ruleById.Values.ToList();

    public LLRules( IEnumerable<LLRule> rules )
    {
        _ruleById = rules.ToDictionary( r => r.Id );
    }

    public LLRule GetRuleByid( int id )
    {
        return _ruleById[ id ];
    }
}
