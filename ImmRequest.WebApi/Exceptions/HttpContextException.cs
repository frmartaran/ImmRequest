using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Exceptions
{
    public class HttpContextException : Exception
    {
        public HttpContextException(string message) : base(message) { }
    }
}
