using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class TextFieldModel : BaseFieldModel<TextField, TextFieldModel>
    {
        public override TextFieldModel SetModel(TextField entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentTypeId = entity.ParentTypeId;
            RangeValues = entity.RangeValues;
            return this;

        }

        public override TextField ToDomain()
        {
            var textField = new TextField
            {
                Name = Name,
                ParentTypeId = ParentTypeId,
            };

            textField.SetRange(RangeValues);
            if (Id.HasValue)
                textField.Id = Id.Value;

            return textField;
        }
    }
}
