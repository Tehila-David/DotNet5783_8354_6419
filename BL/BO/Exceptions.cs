using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class InternalProblemException : Exception
    {
        public InternalProblemException(string message) : base(message)
        {

        }

    }
    public class NullException : Exception
    {
        public NullException(string message) : base(message)
        {

        }

    }
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException(string message, Exception inner) : base(message,inner)
        {

        }

    }

}
