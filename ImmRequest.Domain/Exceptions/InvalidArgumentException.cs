using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Domain.Exceptions
{
    public class InvalidArgumentException : Exception
    {
        public InvalidArgumentException(string message) : base(message) { }

        public InvalidArgumentException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}
