using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class Field : IField
    {
        public string Name { get; set; }
        public DataType DataType { get; set; }
        public ICollection<string> RangeValues { get; set; }

    }
}
