using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Interfaces.Domain
{
    public interface IArea
    {
        string Name { get; set; }

        ICollection<ITopic> Topics { get; set; }
    }
}
