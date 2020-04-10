using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Validators
{
    public class SessionValidator : IValidator<Session>
    {
        private IRepository<Session> Repository { get; set; }

        public SessionValidator(IRepository<Session> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Session objectToValidate)
        {
            return true;
        }
    }
}
