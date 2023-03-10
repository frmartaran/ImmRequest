using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models
{
    public class RequestFieldValuesModel : Model<RequestFieldValues, RequestFieldValuesModel>
    {
        public long Id { get; set; }

        public long ParentCitizenRequestId { get; set; }

        public long FieldId { get; set; }

        public string FieldName { get; set; }

        public List<string> Value { get; set; }

        public override RequestFieldValuesModel SetModel(RequestFieldValues entity)
        {
            Id = entity.Id;
            ParentCitizenRequestId = entity.ParentCitizenRequestId;
            FieldId = entity.FieldId;
            Value = entity.Values;
            FieldName = entity.Field.Name;
            return this;
        }

        public override RequestFieldValues ToDomain()
        {
            return new RequestFieldValues
            {
                Id = Id,
                ParentCitizenRequestId = ParentCitizenRequestId,
                FieldId = FieldId,
                Values = Value
            };
        }
    }
}
