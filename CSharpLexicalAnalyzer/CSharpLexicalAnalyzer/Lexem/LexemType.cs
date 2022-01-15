namespace CSharpLexicalAnalyzer.LexemReflection;

public enum LexemType
{
    //[a-zA-zа-яА-Я]
    Letter,
    //[0-9]
    Digit,
    // <space>|;|.|:|,|{|}|[|]|(|)
    Delimeter,
    // -|*|+|/|%|&|||^|!|~|=|<|>|?|??|::|++|--|&&||||->|==|!=|<=|>=|+=|-=|*=|/=|%=|&=||=|^=|<<|<<=|=>|>>|>>=
    Operator,
    // "
    DoubleQuote,
    // '
    Quote,    
    // \
    BackSlash,
    // /
    Slash,
    // \n|\r\n
    EndOfLine,
    Unknown,
    Underline
}
