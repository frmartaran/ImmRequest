using ImmRequest.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Helpers.Binders
{
    public class CustomTypeConverter : JsonCreationConverter<BaseFieldModel>
    {
        protected override BaseFieldModel Create(Type objectType, JObject jObject)
        {
            if (jObject == null)
                throw new ArgumentNullException("");

            throw new NotImplementedException();
        }
    }
}
