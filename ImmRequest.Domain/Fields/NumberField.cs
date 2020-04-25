using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.Domain.Enums;
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
            IsNotEmpty(values);
            HasTwoValues(values);

            try
            {
                var start = int.Parse(values.First());
                RangeStart = start;

                var end = int.Parse(values.Skip(1).First());
                RangeEnd = end;
            }
            catch (FormatException)
            {
                var message = string.Format(DomainResource.FieldRange_InvalidFormat,
                    DomainResource.Field_Numeric, DataType.Number.ToString());
                throw new InvalidArgumentException(message);
            }

        }

        private static void HasTwoValues(List<string> values)
        {
            if (values.Count > 2)
            {
                var message = string.Format(DomainResource.FieldRange_TooManyValues,
                    2, DomainResource.Field_Numeric);
                throw new InvalidArgumentException(message);
            }
        }

        private static void IsNotEmpty(List<string> values)
        {
            if (values.Count == 0)
                throw new InvalidArgumentException(DomainResource.FieldRange_EmptyValues);
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
            catch (FormatException exception)
            {
                var message = string.Format(DomainResource.Field_InvalidFormat, Name);
                throw new InvalidArgumentException(message, exception);
            }
            catch (OverflowException exception)
            {
                var message = string.Format(DomainResource.Field_InvalidFormat, Name);
                throw new InvalidArgumentException(message, exception);
            }
            IsOutOfRange(numberValue);
            return true;
        }

        private void IsOutOfRange(int numberValue)
        {
            var isValid = numberValue >= RangeStart && numberValue <= RangeEnd;
            if (!isValid)
                throw new DomainValidationException(DomainResource.NumberFieldNotInRangeException);
        }

        public override bool ValidateRangeValues()
        {
            IsEndGreaterThanStart();
            return true;
        }

        private void IsEndGreaterThanStart()
        {
            if (RangeEnd < RangeStart)
                throw new DomainValidationException(DomainResource.FieldRange_StartGreaterThanEnd);
        }
    }
}