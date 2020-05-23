using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class Area : IArea
    {
        public string Name { get; set; }
        public ICollection<ITopic> Topics { get; set; }
    }
}
