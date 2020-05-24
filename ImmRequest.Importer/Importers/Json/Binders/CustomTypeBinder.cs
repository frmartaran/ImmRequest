using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Importers.Json.Binders
{
    public class CustomTypeBinder<T> : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer,  object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
