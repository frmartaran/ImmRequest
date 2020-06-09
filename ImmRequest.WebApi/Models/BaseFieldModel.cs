using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Helpers.Binders;
using ImmRequest.WebApi.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    [JsonConverter(typeof(CustomTypeConverter))]
    public abstract class BaseFieldModel : IModel<BaseField, BaseFieldModel>
    {
        public long? Id { get; set; }
        public string Name { get; set; }

        public long ParentTypeId { get; set; }

        public bool MultipleValues { get; set; }

        public List<string> RangeValues { get; set; }

        public DataType DataType { get; set; }

        public virtual BaseFieldModel SetModel(BaseField entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentTypeId = entity.ParentTypeId;
            MultipleValues = entity.IsMultipleSelectEnabled;
            return this;
        }

        public abstract BaseField ToDomain();
    }
}
