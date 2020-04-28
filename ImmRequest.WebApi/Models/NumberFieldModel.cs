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
            Id = entity.Id;
            Name = entity.Name;
            ParentTypeId = entity.ParentTypeId;
            RangeValues = new List<string>
            {
                entity.RangeStart.ToString(),
                entity.RangeEnd.ToString()
            };

            return this;

        }

        public override NumberField ToDomain()
        {
            var numberField = new NumberField
            {
                Name = Name,
                ParentTypeId = ParentTypeId
            };

            numberField.SetRange(RangeValues);
            if (Id.HasValue)
                numberField.Id = Id.Value;

            return numberField;
        }
    }
}
