// See https://aka.ms/new-console-template for more information
using CSharpLexicalAnalyzer;

namespace CSharpLexicalAnalyzer
{
    public class LexerAutomate
    {
        private ILexerState _currentState;
        private string _buffer;

        public LexerAutomate()
        {

        }

        public async Task<List<TokenInfo>> TokenizeAsync( StreamReader reader )
        {
            while ( reader.Peek() > 0 )
            {
                char character = ( char )reader.Read();
                if ( character == ' ' )
                {
                    if ( _buffer.Any() )
                    {
                        Console.WriteLine( _buffer );
                        _buffer = "";
                    }

                }
                else
                {
                    _buffer += character;
                }
            }

            return null;
        }

        public void SetState( ILexerState state )
        {
            _currentState = state;
        }

        public void StoreTokenInfo( TokenInfo tokenInfo )
        {

        }
    }
}