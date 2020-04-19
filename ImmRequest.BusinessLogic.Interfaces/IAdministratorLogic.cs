using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Interfaces
{
    public interface IAdministratorLogic : ILogic<Administrator>
    {
        Administrator FindAdministratorByCredentials(string email, string password);
    }
}
