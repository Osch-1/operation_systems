using minimizer.Automate;
using minimizer.Automate.Mealy;

namespace minimizer.Minimization
{
    public class AutomateMinimizer
    {
        public Automate<T> GetMinimized<T>( Automate<T> source )
            where T : IState
        {
            if ( typeof( T ) == typeof( MealyState ) )
            {
                return GetMinimizedMealy( source as MealyAutomate );
            }            
        }

        private Automate<MealyState> GetMinimizedMealy( MealyAutomate automate )
        {
            throw new NotImplementedException();
        }
    }
}
