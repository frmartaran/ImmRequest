using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public class NumberField : IIdentifiable, ICustomField
    {
        public long Id { get; set; }
        public long ParentTypeId { get; set; }
        public TopicType ParentType { get; set; }
        public string Name { get; set; }
        public int RangeStart { get; set; }
        public int RangeEnd { get; set; }
        public void SetRange(List<string> values)
        {
            throw new NotImplementedException();
        }
    }
}