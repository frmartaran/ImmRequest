using System;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;

namespace ImmRequest.BusinessLogic
{
    public class AdministratorValidator : IValidator<Administrator>
    {
        private IRepository<Administrator> Repository { get; set; }

        public AdministratorValidator(IRepository<Administrator> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Administrator objectToValidate)
        {
            throw new NotImplementedException();
        }
    }
}
