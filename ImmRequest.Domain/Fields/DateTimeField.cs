using ImmRequest.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ImmRequest.Domain.Resources;
using System.Linq;
using ImmRequest.Domain.Enums;

namespace ImmRequest.Domain.Fields
{
    public class DateTimeField : BaseField
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public override void SetRange(List<string> values)
        {
            IsNotEmpty(values);
            HasTwoValues(values);

            try
            {
                var start = DateTime.Parse(values.First());
                Start = start;

                var end = DateTime.Parse(values.Skip(1).First());
                End = end;
            }
            catch (FormatException)
            {
                var message = string.Format(DomainResource.FieldRange_InvalidFormat,
                    DomainResource.Field_DateTime, DataType.DateTime.ToString());
                throw new InvalidArgumentException(message);
            }
        }

        private static void HasTwoValues(List<string> values)
        {
            if (values.Count > 2)
            {
                var message = string.Format(DomainResource.FieldRange_TooManyValues,
                    2, DomainResource.Field_DateTime);
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
            var modifiedField = valuesToUpdate as DateTimeField;
            Start = modifiedField.Start;
            End = modifiedField.End;
            base.UpdateValues(valuesToUpdate);
        }

        public override bool Validate(string value)
        {
            DateTime dateTimeValue;
            try
            {
                dateTimeValue = JsonConvert.DeserializeObject<DateTime>(value).ToUniversalTime();
            }
            catch (JsonReaderException ex)
            {
                throw ex;
            }
            var isValid = dateTimeValue.Ticks >= Start.ToUniversalTime().Ticks && dateTimeValue.Ticks <= End.ToUniversalTime().Ticks;
            if (!isValid)
            {
                throw new DomainValidationException(DomainResource.DateTimeNotInRangeException);
            }
            return true;
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateRangeValues()
        {
            throw new NotImplementedException();
        }
    }
}