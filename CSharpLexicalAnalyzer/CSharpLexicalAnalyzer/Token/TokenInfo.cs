namespace CSharpLexicalAnalyzer.Token;

public class TokenInfo
{
    private readonly TokenType _tokenType;
    private readonly int _startingPosition;
    private readonly string _value;
    private string _error = "";
    private TokenType _stringLiteral;
    private int _position;
    private string _buffer;

    public TokenType TokenType => _tokenType;
    public int StartingPosition => _startingPosition;
    public string Value => _value;

    public TokenInfo( TokenType tokenType, int startingPosition, string value, string error )
    {
        _tokenType = tokenType;
        _startingPosition = startingPosition;
        _value = value;

        if ( error is not null )
        {
            _error = error;
        }
    }

    public TokenInfo( TokenType tokenType, int startingPosition, string value )
        : this( tokenType, startingPosition, value, null )
    {
    }

    public override string ToString()
    {
        return $"Token type: {_tokenType} Starting position: {_startingPosition} Value: {_value}";
    }
}
