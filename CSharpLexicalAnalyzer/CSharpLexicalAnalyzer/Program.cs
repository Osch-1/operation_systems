// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;
using CSharpLexicalAnalyzer;

Lexer lexerAutomate = new();
string sourceFileName = Console.ReadLine();
string sourceFilePath = Path.Combine( Environment.CurrentDirectory, sourceFileName );
var a = await lexerAutomate.TokenizeAsync( new StreamReader( sourceFilePath ) );

Regex _regex = new( @"^[+-]?\d+$" );

Console.WriteLine( _regex.IsMatch( "+2" ) );