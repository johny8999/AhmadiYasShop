using System;
using System.Runtime.Serialization;

namespace Framework.Application.Exceptions
{
    public class ArgumentInvalidException : Exception
    {
        // public override string Message { get; }
        public ArgumentInvalidException()
        {
        }

        public ArgumentInvalidException(string message) : base(message)
        {

            // message += "Hello";
            //Message=message;

        }

        public ArgumentInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ArgumentInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
