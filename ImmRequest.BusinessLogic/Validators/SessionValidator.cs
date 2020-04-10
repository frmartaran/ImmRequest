using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
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

        private const string Token_PropertyName = "Token";

        public SessionValidator(IRepository<Session> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Session objectToValidate)
        {
            if (Guid.Empty == objectToValidate.Token)
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_IsEmpty, 
                    Token_PropertyName);
                throw new ValidationException(errorMessage);
            }
            return true;
        }
    }
}
