using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface ISession : ILogic<Session>
    {
        bool IsValidToken(Guid token);
    }
}
