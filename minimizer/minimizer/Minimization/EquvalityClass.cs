using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimizer.Minimization
{
    public class EquvalityClass
    {
        private readonly string _name;

        public string Name => _name;

        private Dictionary<string, Dictionary<Signal, string>> _tabledInfo;
    }
}
