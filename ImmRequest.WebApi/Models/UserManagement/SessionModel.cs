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
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid Token { get; set; }

        public override Session ToDomain()
        {
            var newSession = new Session
            {
                Token = Token,
            };

            if (Id.HasValue)
                newSession.Id = Id.Value;

            return newSession;

        }

        public override SessionModel SetModel(Session entity)
        {
            this.Id = entity.Id;
            this.Email = entity.AdministratorInSession.Email;
            this.Password = entity.AdministratorInSession.Password;
            this.Token = entity.Token;
            return this;
        }
    }
}
