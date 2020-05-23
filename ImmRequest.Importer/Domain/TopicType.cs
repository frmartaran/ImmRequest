using ImmRequest.Importer.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class TopicType : IType
    {
        public string Name { get; set; }
        public ICollection<IField> Fields { get; set; }
    }
}
