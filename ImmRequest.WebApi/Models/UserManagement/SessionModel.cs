using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models.UserManagement
{
    public class SessionModel : Model<Session, SessionModel>
    {
        public long? Id { get; set; }

        public AdministratorModel AdministratorInSession { get; set; }

        public Guid Token { get; set; }

        public override Session ToDomain()
        {
            var newSession = new Session
            {
                AdministratorInSession = AdministratorInSession.ToDomain(),
                Token = Token,
            };

            if (Id.HasValue)
                newSession.Id = Id.Value;

            return newSession;

        }

        public override SessionModel SetModel(Session entity)
        {
            throw new NotImplementedException();
        }
    }
}
