using ImmRequest.Domain.Enums;
using System;
using System.Collections.Generic;
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
        public override bool Validate(string value)
        {
            throw new NotImplementedException();
        }
    }
}
