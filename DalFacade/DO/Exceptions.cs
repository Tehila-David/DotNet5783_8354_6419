

namespace DO
{
    /// <summary>
    /// class for excptions of element not exists
    /// </summary>
    public class NotExists: Exception
    {
        public NotExists(string message):base(message)
        {

        }

    }
    /// <summary>
    /// class for excptions of element which exists 
    /// </summary>
    public class AlreadyExists: Exception
    {
        public AlreadyExists(string message):base(message)
        {

        }

    }
    
    [Serializable]
    public class DalConfigException : Exception
    {
        public DalConfigException(string msg) : base(msg) { }
        public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
    }
}
