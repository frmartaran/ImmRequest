using System;
using ImmRequest.Domain.Enums;
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
        public long RequestNumber { get; set; }
        public RequestStatus Status { get; set; }
        public Area Area { get; set; }
        public long AreaId { get; set; }
    }
}
