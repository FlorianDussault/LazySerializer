using System;

namespace LazySerializer
{
    public class SerializerException : Exception
    {
        public SerializerException(string message) : base(message)
        {

        }

        public SerializerException(Exception innerException) : base(innerException.Message, innerException)
        {

        }

        public SerializerException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}