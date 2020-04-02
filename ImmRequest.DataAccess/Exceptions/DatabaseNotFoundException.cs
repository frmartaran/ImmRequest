using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.DataAccess.Exceptions
{
    public class DatabaseNotFoundException : Exception
    {
        public DatabaseNotFoundException(string message) : base(message) { }
    }
}
