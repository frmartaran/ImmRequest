using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Interfaces.Domain
{
    public interface ITopic
    {
        string Name { get; set; }

        long Id { get; set; }

        ICollection<IType> Types { get; set; }
    }
}
