using ImmRequest.Domain.Enums;
using ImmRequest.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Helpers.Binders
{
    public class CustomTypeConverter : JsonCreationConverter<BaseFieldModel>
    {
        protected override BaseFieldModel Create(Type objectType, JObject jObject)
        {
            if (jObject == null)
                throw new ArgumentNullException("");

            var type = (DataType) Convert.ToInt32(jObject["dataType"].ToString());
            if (DataType.Number.CompareTo(type) == 0)
                return new NumberFieldModel();
            if (DataType.Text.CompareTo(type) == 0)
                return new TextFieldModel();
            if (DataType.DateTime.CompareTo(type) == 0)
                return new DateTimeFieldModel();
            if (DataType.Bool.CompareTo(type) == 0)
                return new BoolFieldModel();
            return null;

        }
    }
}
