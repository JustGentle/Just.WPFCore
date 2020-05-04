using System;

namespace Just.Logging
{
    public class FriendlyException : Exception
    {
        public FriendlyException(string message) : base(message)
        {

        }
        public FriendlyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
