using System;
using System.Collections.Generic;
using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain
{
    public class CitizenRequest : IIdentifiable
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string CitizenName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public RequestStatus Status { get; set; }
        public Area Area { get; set; }
        public long AreaId { get; set; }
        public long TopicId { get; set; }
        public Topic Topic { get; set; }
        public long TopicTypeId { get; set; }
        public TopicType TopicType { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<RequestFieldValues> Values { get; set; }

    }
}
