using CSharpLexicalAnalyzer.LexemReflection;
using CSharpLexicalAnalyzer.States;
using CSharpLexicalAnalyzer.Token;
using CSharpLexicalAnalyzer.Token.Reflection;

namespace CSharpLexicalAnalyzer;

public class Lexer : ILexer, ILexerAutomate
{
    private readonly List<TokenInfo> _tokenStorage = new();
    private readonly StreamReader _reader;
    private readonly StateFactory _stateFactory;
    private readonly TokenTypeResolver _tokenTypeResolver;

    private int _currentPos = 1;

    private ILexerState _currentState;
    private string _buffer = string.Empty;

    public string Buffer => _buffer;

    public int Position => _currentPos;

    public Lexer( Stream inputStream )
    {
        if ( inputStream is null )
        {
            throw new ArgumentNullException( nameof( inputStream ) );
        }

        if ( !inputStream.CanRead )
        {
            throw new ArgumentException( "Provided input stream must be readable." );
        }

        _currentState = new EmptyBufferState( this, new LexemTypeResolver() );
        _reader = new StreamReader( inputStream );
        _stateFactory = new StateFactory( this, new LexemTypeResolver() );
        _tokenTypeResolver = new TokenTypeResolver();
    }

    public IReadOnlyList<TokenInfo> TokenizeAsync()
    {
        while ( !IsEndOfTokenization() )
        {
            if ( _reader.EndOfStream )
            {
                OnEndOfInput();
            }
            else
            {
                char character = ( char )_reader.Read();
                _currentState.Next( character );
            }
        }

        _reader.Dispose();
        return _tokenStorage;
    }

    public void MoveCurrentPos()
    {
        _currentPos += _buffer.Length;
    }

    public void SetState( LexerState state )
    {
        _currentState = _stateFactory.GetState( state );
    }

    public void AppendToBuffer( string character )
    {
        if ( character is null )
        {
            throw new ArgumentNullException( nameof( character ) );
        }

        _buffer += character;
    }

    public void StoreTokenInfo( TokenInfo tokenInfo )
    {
        _tokenStorage.Add( tokenInfo );
    }

    public void ClearBuffer()
    {
        _buffer = string.Empty;
    }

    private void OnEndOfInput()
    {
        //throw new Exception( "add finish state validation" );
        SetState( LexerState.FinishState );
    }

    private bool IsEndOfTokenization()
    {
        return _currentState is FinishState;
    }

    private class StateFactory
    {
        private readonly ILexerAutomate _lexerAutomate;
        private readonly LexemTypeResolver _lexemTypeResolver;

        private EmptyBufferState _emptyBufferState;
        private CharacterState _characterState;
        private StringLiteralState _stringLiteralState;
        private IdentifierState _identifierState;
        private NumberState _numberState;
        private DelimeterState _delimeterState;
        private OperatorState _operatorState;
        private SingleLineCommentState _singleLineCommentState;
        private MultiLineCommentState _multiLineCommentState;
        private FinishState _finishState;
        private CommentOrOperatorState _commentOrOperatorState;

        public StateFactory( ILexerAutomate lexerAutomate, LexemTypeResolver lexemTypeResolver )
        {
            _lexerAutomate = lexerAutomate;
            _lexemTypeResolver = lexemTypeResolver;
        }

        public ILexerState GetState( LexerState stateName )
        {
            switch ( stateName )
            {
                case LexerState.EmptyBuffer:
                    if ( _emptyBufferState == null )
                    {
                        _emptyBufferState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _emptyBufferState;

                case LexerState.Character:
                    if ( _characterState == null )
                    {
                        _characterState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _characterState;
                case LexerState.StringLiteral:
                    if ( _stringLiteralState == null )
                    {
                        _stringLiteralState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _stringLiteralState;
                case LexerState.Identifier:
                    if ( _identifierState == null )
                    {
                        _identifierState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _identifierState;
                case LexerState.Number:
                    if ( _numberState == null )
                    {
                        _numberState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _numberState;
                case LexerState.Delimeter:
                    if ( _delimeterState == null )
                    {
                        _delimeterState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _delimeterState;
                case LexerState.Operator:
                    if ( _operatorState == null )
                    {
                        _operatorState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _operatorState;
                case LexerState.SingleLineComment:
                    if ( _singleLineCommentState == null )
                    {
                        _singleLineCommentState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _singleLineCommentState;
                case LexerState.MultiLineComment:
                    if ( _multiLineCommentState == null )
                    {
                        _multiLineCommentState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _multiLineCommentState;
                case LexerState.FinishState:
                    if ( _finishState == null )
                    {
                        _finishState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _finishState;
                case LexerState.CommentOrOperator:
                    if ( _commentOrOperatorState == null )
                    {
                        _commentOrOperatorState = new( _lexerAutomate, _lexemTypeResolver );
                    }
                    return _commentOrOperatorState;
                default:
                    throw new Exception( $"Unsupported state has been provided: {stateName}" );
            }
        }
    }
}
