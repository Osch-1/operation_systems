namespace LLParser;

public interface ILLRulesReader
{
    public LLRules Read();
}

public class LLRulesReader : ILLRulesReader
{
    private const string _tokensDelimeter = " ";
    private const string _noRuleId = "null";

    private Stream _inputStream;

    public LLRulesReader( Stream inputStream )
    {
        if ( !inputStream.CanRead )
        {
            throw new ArgumentException( "InputStream must be readable" );
        }

        _inputStream = inputStream;
    }

    public LLRules Read()
    {
        using StreamReader stream = new( _inputStream );

        List<LLRule> rules = new();

        string? line;
        while ( ( line = stream.ReadLine() ) is not null )
        {
            string[] tokens = line.Split( _tokensDelimeter );

            if ( tokens.Length != 8 )
            {
                throw new FormatException();
            }

            int id = Convert.ToInt32( tokens[ 0 ] );
            IEnumerable<string> guideSymbols = tokens[ 1 ].Select( c => c.ToString() );
            bool shift = Convert.ToBoolean( tokens[ 2 ] );
            bool throwIfNotMatch = Convert.ToBoolean( tokens[ 3 ] );
            int? nextRuleId = tokens[ 4 ] == _noRuleId ? null : Convert.ToInt32( tokens[ 4 ] );
            bool stackNextRule = Convert.ToBoolean( tokens[ 5 ] );
            bool isEnd = Convert.ToBoolean( tokens[ 6 ] );
            bool isSupportNull = Convert.ToBoolean( tokens[ 7 ] );

            rules.Add( new LLRule( id, guideSymbols, shift, throwIfNotMatch, nextRuleId, stackNextRule, isEnd, isSupportNull ) );
        }

        return new LLRules( rules );
    }
}