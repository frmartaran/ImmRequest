using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Domain.Fields
{
    public class BoolField : BaseField
    {
        public override DataType InputType { get { return DataType.Bool; } }

        public override bool IsMultipleSelectEnabled
        {
            get { return false; }
            set { return; }
        }
        public override bool Validate(List<string> values)
        {
            try
            {
                Convert.ToBoolean(values.First());
                return true;

            }
            catch (FormatException)
            {
                var message = string.Format(DomainResource.FieldRange_InvalidFormat,
                    DomainResource.Field_Bool, DataType.Bool.ToString());
                throw new InvalidArgumentException(message);
            }
        }
    }
}
