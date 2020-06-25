using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Exceptions
{
    public class DeveloperException : Exception
    {
        public DeveloperException(string message, Exception exception) : base(message, exception) { }

        public DeveloperException(string message) : base(message) { }

    }
}
