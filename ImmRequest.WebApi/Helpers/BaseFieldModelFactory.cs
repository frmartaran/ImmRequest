using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Helpers
{
    public class BaseFieldModelFactory
    {

        public static BaseFieldModel GetFieldModel(BaseField field)
        {
            return field.InputType switch
            {
                DataType.Number => new NumberFieldModel().SetModel(field),
                DataType.Text => new TextFieldModel().SetModel(field),
                DataType.DateTime => new DateTimeFieldModel().SetModel(field),
                DataType.Bool => new BoolFieldModel().SetModel(field),
                _ => null,
            };
        }
    }
}
