using LLParser;

string inputFileName = Console.ReadLine() ?? throw new ArgumentNullException();
string inputFilePath = Path.Combine( Environment.CurrentDirectory, inputFileName );

FileStream inputFileStream = File.OpenRead( inputFilePath );

ILLRulesReader reader = new LLRulesReader( inputFileStream );

LLParser.LLParser parser = new( reader );
//parser.IsMatch()