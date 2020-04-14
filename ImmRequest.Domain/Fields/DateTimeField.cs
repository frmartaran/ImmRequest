using ImmRequest.Domain.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        public override bool Validate(string value)
        {
            try
            {
                var dateTimeValue = JsonConvert.DeserializeObject<DateTime>(value).ToUniversalTime();
                return dateTimeValue.Ticks >= Start.ToUniversalTime().Ticks && dateTimeValue.Ticks <= End.ToUniversalTime().Ticks;
            }
            catch(Exception ex)
            {
                throw new ValidationException(ex.ToString());
            }            
        }

        public override T ValueToDataType<T>(string value)
        {
            throw new NotImplementedException();
        }
    }
}