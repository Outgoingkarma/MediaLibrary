namespace MediaLibrary.Core
{
    public class InvalidMediaException : Exception
    {
        public InvalidMediaException(string message) : base(message)
        {
        }
        
        public InvalidMediaException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}