using System;
using System.Collections.Generic;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.Fields
{
    public class TextField : BaseField
    {
        public List<string> RangeValues { get; set; }
        public override void SetRange(List<string> values)
        {
            throw new NotImplementedException();
        }

        public override void Validate(string value)
        {
            throw new NotImplementedException();
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }
    }
}