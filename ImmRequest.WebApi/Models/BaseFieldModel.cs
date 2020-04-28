using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public abstract class BaseFieldModel<T, TM> : Model<T, TM>
        where T : BaseField, new()
        where TM : Model<T, TM>, new()
    {

        public long Id { get; set; }
        public string Name { get; set; }

        public long ParentTypeId { get; set; }

        public List<string> RangeValues { get; set; }

    }
}
