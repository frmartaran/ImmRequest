using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Interfaces.Exceptions
{
    public class FileLoadFailureException : Exception
    {
        public FileLoadFailureException(string message) : base(message) { }
    }
}
