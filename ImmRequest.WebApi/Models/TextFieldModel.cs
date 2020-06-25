using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImmRequest.Domain.Enums;

namespace ImmRequest.WebApi.Models
{
    public class TextFieldModel : BaseFieldModel
    {
        public override BaseFieldModel SetModel(BaseField entity)
        {
            var asText = entity as TextField;
            DataType = DataType.Text;
            Id = entity.Id;
            Name = entity.Name;
            ParentTypeId = entity.ParentTypeId;
            RangeValues = asText.RangeValues;
            return this;
        }

        public override BaseField ToDomain()
        {
            var field = new TextField
            {
                Name = Name,
                ParentTypeId = ParentTypeId,
                IsMultipleSelectEnabled = MultipleValues
            };

            field.SetRange(RangeValues);
            if (Id.HasValue)
                field.Id = Id.Value;

            return field;
        }
    }
}
