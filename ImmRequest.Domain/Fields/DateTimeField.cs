using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public class DateTimeField : IIdentifiable, ICustomField
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ParentTypeId { get; set; }
        public TopicType ParentType { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public void SetRange(List<string> values)
        {
            throw new NotImplementedException();
        }
    }
}