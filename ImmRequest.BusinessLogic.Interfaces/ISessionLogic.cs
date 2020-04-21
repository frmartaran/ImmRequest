using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface ISessionLogic
    {
        bool IsValidToken(Guid token);

        Guid Create(Session session);

        Session Get(Guid token);

        void Delete(long id);

    }
}
