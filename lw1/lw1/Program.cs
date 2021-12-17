// See https://aka.ms/new-console-template for more information

using System.Reflection;
using lw1;

string path = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), @"in1.txt" );

AutomateReader reader = new( new StreamReader( path ) );
Automate automate = reader.GetAutomate();
Console.WriteLine( automate );