using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using ImmRequest.Domain.Enums;

namespace ImmRequest.WebApi.Models
{
    public class DateTimeFieldModel : BaseFieldModel
    {
        public override BaseFieldModel SetModel(BaseField entity)
        {
            var asDateField = entity as DateTimeField;
            DataType = DataType.DateTime;
            RangeValues = new List<string>
            {
                asDateField.Start.ToString(),
                asDateField.End.ToString(),
            };
            return base.SetModel(entity);
        }
        public override BaseField ToDomain()
        {
            var field = new DateTimeField
            {
                Name = Name,
                ParentTypeId = ParentTypeId
            };
            field.SetRange(RangeValues);

            if (Id.HasValue)
                field.Id = Id.Value;

            return field;
        }
    }
}
