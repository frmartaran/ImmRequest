using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public class DateTimeField : BaseField
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public override void SetRange(List<string> values)
        {
            throw new NotImplementedException();
        }
    }
}