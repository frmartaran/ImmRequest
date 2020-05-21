using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.DataAccess.Interfaces.Exceptions
{
    public class DBDeveloperException : Exception
    {
        public DBDeveloperException(string message) : base(message) { }
    }
}
