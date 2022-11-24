

namespace DO
{
    /// <summary>
    /// class for excptions of element not exists
    /// </summary>
    public class NotExists: Exception
    {
        public NotExists(string message)
        {

        }

    }
    /// <summary>
    /// class for excptions of element which exists 
    /// </summary>
    public class AlreadyExists: Exception
    {
        public AlreadyExists(string message)
        {

        }

    }
}
