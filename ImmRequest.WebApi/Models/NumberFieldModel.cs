using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.Domain.Enums;
namespace ImmRequest.WebApi.Models
{
    public class NumberFieldModel : BaseFieldModel
    {
        public override BaseFieldModel SetModel(BaseField entity)
        {
            var numberField = entity as NumberField;
            DataType = DataType.Number;
            RangeValues = new List<string> {
                numberField.RangeStart.ToString(),
                numberField.RangeEnd.ToString()
            };

            return base.SetModel(entity);

        }

        public override BaseField ToDomain()
        {
            var field = new NumberField
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
