// See https://aka.ms/new-console-template for more information
namespace CSharpLexicalAnalyzer
{
    public enum TokenType
    {
        //letter|(identifier)[letter|digit]*
        Identifier,
        // '{'|'}'|'['|']'|'('|')'|'.'|','|':'|';'
        // '+'|'-'|'*'|'/'|'%'|'&'|'|'|'^'|'!'|'~'
        // '='|'<'|'>'|'?'|'??'|'::'|'++'|'--'|'&&'|'||'
        // '->'|'=='|'!='|'<='|'>='|'+='|'-='|'*='|'/='|'%='
        // '&='|'|='|'^='|'<<'|'<<='|'=>'|'>>'|'>>='
        OperatorOrPunctuator,
        StringLiteral,
        IntegerLiteral,
        //RealLiteral
    }    
}