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

            var type = jObject["DataType"].ToString().ToUpper();
            if (type == DataType.Number.ToString().ToUpper())
                return new NumberFieldModel();
            return null;
        }
    }
}
