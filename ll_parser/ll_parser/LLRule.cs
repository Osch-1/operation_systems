namespace LLParser;

public class LLRule
{
    public int Id { get; init; }
    public List<string> GuideSymbols { get; init; }
    public bool Shift { get; init; }
    public bool ThrowIfNotMatch { get; init; }
    public int? NextRuleId { get; init; }
    public bool StackNextRule { get; init; }
    public bool IsEnd { get; init; }
    public bool IsAcceptNull { get; init; }

    public LLRule( int id, IEnumerable<string> guideSymbols, bool shift, bool throwIfNotMatch, int? nextRuleId, bool stackNextRule, bool isEnd, bool isSupportNull )
    {
        Id = id;
        GuideSymbols = guideSymbols.ToList();
        Shift = shift;
        ThrowIfNotMatch = throwIfNotMatch;
        NextRuleId = nextRuleId;
        StackNextRule = stackNextRule;
        IsEnd = isEnd;
        IsAcceptNull = isSupportNull;
    }
}
