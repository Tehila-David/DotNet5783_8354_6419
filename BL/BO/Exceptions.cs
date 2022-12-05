using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class InternalProblem : Exception
    {
        public InternalProblem(string message) : base(message)
        {

        }
        public InternalProblem(string message, Exception inner) : base(message, inner)
        {

        }

    }
    
