using ImmRequest.Importer.Extentions;
using ImmRequest.Importer.Interfaces.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class TopicType : IType
    {
        public TopicType() { }

        [JsonConstructor]
        public TopicType(List<Field> fields)
        {
            Fields = fields.ToInterfaceList<IField, Field>();
        }
        public string Name { get; set; }

        public ICollection<IField> Fields { get; set; }
    }
}
