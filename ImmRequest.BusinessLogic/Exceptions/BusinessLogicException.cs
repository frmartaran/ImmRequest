using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Exceptions
{
    public class BusinessLogicException : Exception
    {
        public BusinessLogicException(string message, Exception innerException)
            : base(message, innerException) { }

        public BusinessLogicException(string message) : base(message) { }
    }
}
