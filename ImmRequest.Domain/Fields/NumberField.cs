using System;
using System.Collections.Generic;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Resources;

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
            int numberValue;
            try
            {
                numberValue = Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var isValid = numberValue >= RangeStart && numberValue <= RangeEnd;
            if (!isValid)
                throw new ValidationException(DomainResource.NumberFieldNotInRangeException);
            return true;
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }
    }
}