using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface ISessionLogic : ILogic<Session>
    {
        bool IsValidToken(Guid token);
    }
}
