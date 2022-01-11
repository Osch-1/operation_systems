// See https://aka.ms/new-console-template for more information

using CSharpLexicalAnalyzer;

LexerAutomate lexerAutomate = new();
string sourceFileName = Console.ReadLine();
string sourceFilePath = Path.Combine( Environment.CurrentDirectory, sourceFileName );
var a = await lexerAutomate.TokenizeAsync( new StreamReader( sourceFilePath ) );