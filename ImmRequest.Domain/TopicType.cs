using System;
using System.Collections.Generic;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class TopicType : IIdentifiable, ISoftDelete
    {
        public long Id { get; set; }
        public long ParentTopicId { get; set; }
        public Topic ParentTopic { get; set; }
        public string Name { get; set; }
        public ICollection<BaseField> AllFields { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
    }
}