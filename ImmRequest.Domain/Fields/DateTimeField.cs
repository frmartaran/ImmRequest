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

        public override void UpdateValues(BaseField valuesToUpdate)
        {
            var modifiedField = valuesToUpdate as DateTimeField;
            Start = modifiedField.Start;
            End = modifiedField.End;
            base.UpdateValues(valuesToUpdate);
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