using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class NumberFieldModel : BaseFieldModel<NumberField, NumberFieldModel>
    {
        public override NumberFieldModel SetModel(NumberField entity)
        {
            throw new NotImplementedException();
        }

        public override NumberField ToDomain()
        {
            throw new NotImplementedException();
        }
    }
}
