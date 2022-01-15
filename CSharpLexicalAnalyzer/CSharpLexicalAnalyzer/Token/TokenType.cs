namespace CSharpLexicalAnalyzer.Token;

public enum TokenType
{
    Unknown,
    Character,
    StringLiteral,
    Identifier,
    Number,
    Delimeter,
    Operator,
    Comment,
    EndOfLine,
    Keyword
}
