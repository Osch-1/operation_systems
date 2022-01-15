namespace CSharpLexicalAnalyzer;

public enum LexerState
{
    EmptyBuffer,
    Character,
    StringLiteral,
    Identifier,
    Number,
    Delimeter,
    CommentOrOperator,
    Operator,
    SingleLineComment,
    MultiLineComment,
    FinishState
}