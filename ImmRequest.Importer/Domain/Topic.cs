using ImmRequest.Importer.Extentions;
using ImmRequest.Importer.Interfaces.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class Topic : ITopic
    {
        public Topic()
        {

        }

        [JsonConstructor]
        public Topic(List<TopicType> types)
        {
            Types = types.ToInterfaceList<IType, TopicType>();
        }
        public string Name { get; set; }
        public long Id { get; set; }

        public ICollection<IType> Types { get; set; }
    }
}
