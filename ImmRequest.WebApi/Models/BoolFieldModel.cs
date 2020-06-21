using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class BoolFieldModel : BaseFieldModel
    {
        public override BaseFieldModel SetModel(BaseField entity)
        {
            DataType = DataType.Bool;
            return base.SetModel(entity);
        }
        public override BaseField ToDomain()
        {
            var field = new BoolField
            {
                Name = this.Name
            };

            if (Id.HasValue)
                field.Id = Id.Value;

            return field;
        }
    }
}
