using System;
using System.Collections.Generic;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class TopicType : IIdentifiable
    {
        public long Id { get; set; }
        public long ParentTopicId { get; set; }
        public Topic ParentTopic { get; set; }
        public string Name { get; set; }
        public List<BaseField> AllFields { get; set; }
        public long RequestId { get; set; }
    }
}