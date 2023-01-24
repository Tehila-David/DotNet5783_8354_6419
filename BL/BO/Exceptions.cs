using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class InternalProblem : Exception
    {
        public InternalProblem(string message) : base(message) { }
        public InternalProblem(string message, Exception inner) : base(message, inner) { }
    }

    [Serializable]
    internal class EntityNotExist : Exception
    {
        public EntityNotExist()
        {
        }

        public EntityNotExist(string? message) : base(message)
        {
        }

        public EntityNotExist(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EntityNotExist(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
    public class notEnoughInStock : Exception
    {
        public notEnoughInStock(string message) : base(message) { }

    }
    public class IncorrectName : Exception
    {
        public IncorrectName(string message) : base(message) { }

    }
    public class IncorrectAddress : Exception
    {
        public IncorrectAddress(string message) : base(message) { }

    }
    public class IncorrectEmailAddress : Exception
    {
        public IncorrectEmailAddress(string message) : base(message) { }

    }
    public class negativeProductAmount : Exception
    {
        public negativeProductAmount(string message) : base(message) { }

    }

}

