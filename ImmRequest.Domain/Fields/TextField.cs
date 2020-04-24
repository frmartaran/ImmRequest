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
            if (values.Count == 0)
                throw new InvalidArgumentException(DomainResource.FieldRange_EmptyValues);
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
                throw new DomainValidationException(DomainResource.TextFieldNotInRangeException);
            return true;
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }
    }
}