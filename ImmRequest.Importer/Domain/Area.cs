using ImmRequest.Importer.Extentions;
using ImmRequest.Importer.Interfaces.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Domain
{
    public class Area : IArea
    {
        public Area()
        {

        }

        [JsonConstructor]
        public Area(List<Topic> topics)
        {
            Topics = topics.ToInterfaceList<ITopic, Topic>();
        }
        public string Name { get; set; }
        public long Id { get; set; }
        public ICollection<ITopic> Topics { get; set; }
    }
}
