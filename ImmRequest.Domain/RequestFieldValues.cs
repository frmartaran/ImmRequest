using System;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class RequestFieldValues : IIdentifiable
    {
        public long Id { get; set; }

        public long CitizenRequestId { get; set; }

        public CitizenRequest ParentCitizenRequest { get; set; }

        public long FieldId { get; set; }

        public string Value { get; set; }
    }
}