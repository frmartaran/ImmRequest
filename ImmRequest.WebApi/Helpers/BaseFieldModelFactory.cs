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
            if (field.Type == DataType.Number)
                return new NumberFieldModel().SetModel(field);
            else if (field.Type == DataType.Number)
                return new DateTimeFieldModel().SetModel(field);
            else if (field.Type == DataType.Number)
                return new TextFieldModel().SetModel(field);
            else return null;
        }
    }
}
