// See https://aka.ms/new-console-template for more information
using System.Reflection;
using minimizer.Automate.Mealy;

Console.WriteLine( "Hello, World!" );

string path = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), @"mealy.txt" );

MealyAutomate automate = MealyAutomate.FromStream( new StreamReader( path ) );
Console.WriteLine( automate );

SignalToAction s1 = new( new( "q0" ), new minimizer.Mealy.Action( new( "q1" ), new Output( "z1" ) ) );
SignalToAction s2 = new( new( "q0" ), new minimizer.Mealy.Action( new( "q1" ), new Output( "z1" ) ) );
HashSet<SignalToAction> a = new HashSet<SignalToAction>() { s1, s2 };

SignalToAction s3 = new( new( "q0" ), new minimizer.Mealy.Action( new( "q1" ), new Output( "z1" ) ) );
SignalToAction s4 = new( new( "q0" ), new minimizer.Mealy.Action( new( "q1" ), new Output( "z1" ) ) );
HashSet<SignalToAction> b = new HashSet<SignalToAction>() { s3, s4 };

List<HashSet<SignalToAction>> abobus = new List<HashSet<SignalToAction>> { a, b };

var abobusGroup = abobus.GroupBy(SequenceEquals);

Console.WriteLine( a.SequenceEqual( b ) );