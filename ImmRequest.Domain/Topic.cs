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
        public List<TopicType> Types { get; set; }
        public long RequestId { get; set; }
    }
}