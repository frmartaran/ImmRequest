using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class Topic : IIdentifiable
    {
        public long Id { get; set; }
        public long ParentAreaId { get; set; }
        public Area ParentArea { get; set; }
        public List<TopicType> Types { get; set; }

    }
}