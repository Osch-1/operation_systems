using System.Text;

namespace minimizer.Automate.Mealy
{
    public class MealyAutomate : Automate<MealyState>
    {
        public override Automate<MealyState> FromStream( Stream stream )
        {
            StreamReader sr = new StreamReader( stream );
            string type = sr.ReadLine();
            if ( type != "Mealy" )
            {
                throw new ArgumentException( "Expected Mealy automate but Moore has been found." );
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder sb = new( 300 );

            List<string> list = new();
            sb.AppendLine( "Mealy" );
            foreach ( MealyState state in States )
            {
                
            }

            return sb.ToString();
        }
    }
}
