using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class Topic : IIdentifiable
    {
        public long Id { get; set; }
        public long AreaId { get; set; }
        public Area Area { get; set; }
        public string Name { get; set; }
        public ICollection<TopicType> Types { get; set; }
    }
}