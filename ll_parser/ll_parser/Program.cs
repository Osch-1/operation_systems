using LLParser;

string? inputFileName = Console.ReadLine();
if ( inputFileName is null )
{
    return;
}
string inputFilePath = Path.Combine( Environment.CurrentDirectory, inputFileName );

FileStream inputFileStream = File.OpenRead( inputFilePath );

ILLRulesReader reader = new LLRulesReader( inputFileStream );
LLParser.LLParser parser = new( reader );

string? line = "";
while ( ( line = Console.ReadLine() ) != null )
{
    bool isMatch = parser.IsMatch( line );
    string noun = isMatch ? "is" : "isn't";

    Console.WriteLine( $"{line} {noun} matches the rules" );
}
