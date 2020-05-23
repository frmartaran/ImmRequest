using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Interfaces.Domain
{
    public interface IField
    {
        string Name { get; set; }

        ICollection<string> RangeValues { get; set; }
    }
}
