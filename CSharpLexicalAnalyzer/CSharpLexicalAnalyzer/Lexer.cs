// See https://aka.ms/new-console-template for more information
using CSharpLexicalAnalyzer;

namespace CSharpLexicalAnalyzer
{
    public class Lexer : ILexer, ILexerAutomate
    {
        private ILexerState _currentState;
        private string _buffer;

        public string Buffer => _buffer;


        private HashSet<string> _operators = new()
        {
            "-",
            "*",
            "+",
            "/",
            "%",
            "&",
            "|",
            "^",
            "!",
            "~",
            "=",
            "<",
            ">",
            "?",
            "??",
            "::",
            "++",
            "--",
            "&&",
            "||",
            "->",
            "==",
            "!=",
            "<=",
            ">=",
            "+=",
            "-=",
            "*=",
            "/=",
            "%=",
            "&=",
            "|=",
            "^=",
            "<<",
            "<<=",
            "=>",
            ">>",
            ">>="
        };

        private HashSet<string> _delimeters = new()
        {
            " ",
            "'",
            "\"",
            "\\",
            "{",
            "}",
            "[",
            "]",
            "(",
            ")",
            ".",
            ",",
            ":",
            ";"
        };

        public Lexer()
        {

        }

        public List<TokenInfo> TokenizeAsync( StreamReader reader )
        {
            while ( reader.Peek() > 0 )
            {
                _currentState.Next( reader.Read().ToString() );
            }

            return new();
        }

        public void SetState( ILexerState state )
        {
            _currentState = state;
        }


        public void ClearBuffer()
        {
            throw new NotImplementedException();
        }

        public void SetBuffer( string buffer )
        {
            throw new NotImplementedException();
        }

        public void AddTokenInfo( TokenInfo tokenInfo )
        {
            throw new NotImplementedException();
        }
    }
}