using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmRequest.WebApi.Models.UserManagement
{
    public class SessionModel : IModel<Session, SessionModel>
    {
        public long Id { get; set; }

        public AdministratorModel AdministratorInSession { get; set; }

        public Guid Token { get; set; }

        public Session ToDomain()
        {
            throw new NotImplementedException();
        }

        public SessionModel ToModel(Session entity)
        {
            throw new NotImplementedException();
        }
    }
}
