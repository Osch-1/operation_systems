namespace LLParser;

public class LLParser
{
    LLRules rules;
    public LLParser( ILLRulesReader reader )
    {
        rules = reader.Read();
    }

    public bool IsMatch( string input )
    {
        if ( input is null )
        {
            return false;
        }


    }
}
