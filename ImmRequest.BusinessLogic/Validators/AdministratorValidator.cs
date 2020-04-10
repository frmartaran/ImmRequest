using System;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain.UserManagement;

namespace ImmRequest.BusinessLogic
{
    public class AdministratorValidator : IValidator<Administrator>
    {
        public bool IsValid(Administrator objectToValidate)
        {
            throw new NotImplementedException();
        }
    }
}
