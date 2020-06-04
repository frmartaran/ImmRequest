using System;
using System.Collections.Generic;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class RequestFieldValues : IIdentifiable
    {
        public long Id { get; set; }

        public long ParentCitizenRequestId { get; set; }

        public CitizenRequest ParentCitizenRequest { get; set; }

        public long FieldId { get; set; }

        public BaseField Field { get; set; }

        public List<string> Values { get; set; }
    }
}