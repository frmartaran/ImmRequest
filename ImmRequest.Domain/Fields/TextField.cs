using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Resources;

namespace ImmRequest.Domain.Fields
{
    public class TextField : BaseField
    {
        public override DataType InputType { get { return DataType.Text; } }
        public List<string> RangeValues { get; set; }
        public override void SetRange(List<string> values)
        {
            if (values.Count == 0)
                throw new InvalidArgumentException(DomainResource.FieldRange_EmptyValues);

            RangeValues = values;
        }

        public override void UpdateValues(BaseField valuesToUpdate)
        {
            var modifiedField = valuesToUpdate as TextField;
            RangeValues = modifiedField.RangeValues;
            base.UpdateValues(valuesToUpdate);
        }

        public override bool Validate(List<string> values)
        {
            if (!IsMultipleSelectEnabled && values.Count > 1)
                ExceptionThrowerHelper.ThrowMultipleSelectionDisable(Name);

            foreach(var value in values)
            {
                if (!RangeValues.Contains(value))
                    throw new DomainValidationException(DomainResource.TextFieldNotInRangeException);

            }
            return true;
        }

        public override bool ValidateRangeValues()
        {
            return true;
        }
    }
}