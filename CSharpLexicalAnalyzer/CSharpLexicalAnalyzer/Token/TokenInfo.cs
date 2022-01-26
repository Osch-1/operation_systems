using System.Text;

namespace CSharpLexicalAnalyzer.Token;

public class TokenInfo
{
    private readonly TokenType _tokenType;
    private readonly int _startingPosition;
    private readonly string _value;
    private string _error = "";
    private readonly int _lineNumber;

    public TokenInfo( TokenType tokenType, int startingPosition, string value, int lineNumber, string error )
    {
        _tokenType = tokenType;
        _startingPosition = startingPosition;
        _value = value;
        _lineNumber = lineNumber;

        if ( error is not null )
        {
            _error = error;
        }
    }

    public TokenInfo( TokenType tokenType, int startingPosition, string value, int lineNumber )
        : this( tokenType, startingPosition, value, lineNumber, null )
    {
    }

    public override string ToString()
    {
        StringBuilder sb = new( 200 );
        sb.Append( $"Token type: {_tokenType} Line number: {_lineNumber} Starting position: {_startingPosition} Value: {_value}" );
        if ( _error is not null )
        {
            sb.Append( _error );
        }

        return sb.ToString();
    }
}
