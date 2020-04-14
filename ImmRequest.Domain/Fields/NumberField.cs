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

        public override void UpdateValues(BaseField valuesToUpdate)
        {
            var modifiedField = valuesToUpdate as NumberField;
            RangeStart = modifiedField.RangeStart;
            RangeEnd = modifiedField.RangeEnd;
            base.UpdateValues(valuesToUpdate);
        }

        public override bool Validate(string value)
        {
            throw new NotImplementedException();
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }
    }
}