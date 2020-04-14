using System;
using System.Collections.Generic;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Resources;

namespace ImmRequest.Domain.Fields
{
    public class TextField : BaseField
    {
        public List<string> RangeValues { get; set; }
        public override void SetRange(List<string> values)
        {
            throw new NotImplementedException();
        }

        public override void UpdateValues(BaseField valuesToUpdate)
        {
            var modifiedField = valuesToUpdate as TextField;
            RangeValues = modifiedField.RangeValues;
            base.UpdateValues(valuesToUpdate);
        }

        public override bool Validate(string value)
        {
            if (!RangeValues.Contains(value))
                throw new ValidationException(DomainResource.TextFieldNotInRangeException);
            return true;
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }
    }
}