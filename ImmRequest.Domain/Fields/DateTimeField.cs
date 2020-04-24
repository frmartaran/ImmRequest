using ImmRequest.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ImmRequest.Domain.Resources;
using System.Linq;

namespace ImmRequest.Domain.Fields
{
    public class DateTimeField : BaseField
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public override void SetRange(List<string> values)
        {
            if (values.Count == 0)
                throw new InvalidArgumentException(DomainResource.FieldRange_EmptyValues);

            var start = DateTime.Parse(values.First());
            Start = start;

            var end = DateTime.Parse(values.Skip(1).First());
            End = end;


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
    }
}