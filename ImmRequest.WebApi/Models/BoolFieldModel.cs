using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class BoolFieldModel : BaseFieldModel
    {
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
