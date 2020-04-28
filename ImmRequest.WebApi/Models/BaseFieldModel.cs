using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public abstract class BaseFieldModel : IModel<BaseField, BaseFieldModel>
    {
        public long? Id { get; set; }
        public string Name { get; set; }

        public long ParentTypeId { get; set; }

        public List<string> RangeValues { get; set; }

        public virtual BaseFieldModel SetModel(BaseField entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            ParentTypeId = entity.ParentTypeId;
            return this;
        }

        public abstract BaseField ToDomain();
    }
}
