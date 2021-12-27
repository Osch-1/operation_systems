using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimizer.Automate
{
    public class Automate<T> where T : IState
    {
        private readonly List<T> _states;

        public List<T> States => _states;

        public Automate()
        {
            _states = new();
        }

        public void AddState( T state )
        {
            _states.Add( state );
        }
    }
}
