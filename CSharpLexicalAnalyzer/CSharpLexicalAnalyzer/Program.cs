using CSharpLexicalAnalyzer.Token;

namespace CSharpLexicalAnalyzer;

public class Program
{
    private static readonly string _readFilePattern = "Enter {0} file name: ";

    public static async Task Main( string[] args )
    {
        Console.Write( string.Format( _readFilePattern, "input" ) );
        string sourceFileName = Console.ReadLine();
        Console.WriteLine();

        Console.Write( string.Format( _readFilePattern, "output" ) );
        string outputFileName = Console.ReadLine();
        Console.WriteLine();

        string sourceFilePath = Path.Combine( Environment.CurrentDirectory, $"{sourceFileName}.txt" );
        FileStream sourceFileStream = File.Open( sourceFilePath, FileMode.Open );

        string outputFilePath = Path.Combine( Directory.GetParent( Directory.GetCurrentDirectory() ).Parent.Parent.Parent.FullName, $"{outputFileName}.txt" );
        FileStream outputFileStream = File.Open( outputFilePath, FileMode.Create );

        Lexer lexerAutomate = new( sourceFileStream );

        StreamWriter _output = new( outputFileStream )
        {
            AutoFlush = true
        };
        IReadOnlyList<TokenInfo> tokensInfo = lexerAutomate.TokenizeAsync();

        int tokenNumber = 0;
        foreach ( TokenInfo tokenInfo in tokensInfo )
        {
            await _output.WriteLineAsync( $"{tokenNumber}.{tokenInfo}" );
            tokenNumber++;
        }
    }
}
