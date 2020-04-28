﻿using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class DateTimeFieldModel : BaseFieldModel
    {
        public override BaseFieldModel SetModel(BaseField entity)
        {
            var asDateField = entity as DateTimeField;
            RangeValues = new List<string>
            {
                asDateField.Start.ToString(),
                asDateField.End.ToString(),
            };
            return base.SetModel(entity);
        }
        public override BaseField ToDomain()
        {
            throw new NotImplementedException();
        }
    }
}
