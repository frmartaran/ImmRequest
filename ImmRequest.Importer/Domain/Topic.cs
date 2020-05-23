using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class Topic : ITopic
    {
        public string Name { get; set; }
        public ICollection<IType> Types { get; set; }
    }
}
