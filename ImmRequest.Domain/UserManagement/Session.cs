using System;
using ImmRequest.Domain.Interfaces;

namespace ImmRequest.Domain.UserManagement
{
    public class Session : IIdentifiable
    {
        public long Id { get; set; }

        public Guid Token { get; set; }

        public Administrator AdministratorInSession { get; set; }

        public long AdministratorInSessionId { get; set; }
    }
}