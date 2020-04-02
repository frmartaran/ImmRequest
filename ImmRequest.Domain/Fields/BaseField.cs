using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public abstract class BaseField : ICustomField, IIdentifiable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentTypeId { get; set; }
        public TopicType ParentType { get; set; }
        public long RequestId { get; set; }
        public abstract void SetRange(List<string> values);
    }
}