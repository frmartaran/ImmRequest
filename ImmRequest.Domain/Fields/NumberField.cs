using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public class NumberField : BaseField
    {
        public int RangeStart { get; set; }
        public int RangeEnd { get; set; }
        public override void SetRange(List<string> values)
        {
            throw new NotImplementedException();
        }
    }
}