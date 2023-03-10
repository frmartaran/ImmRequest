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
        private const string Admin_PropertyName = "Administrator";

        public SessionValidator(IRepository<Session> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Session objectToValidate)
        {
            ValidateAdministratorLoggedIn(objectToValidate);
            return true;
        }

        private void ValidateAdministratorLoggedIn(Session objectToValidate)
        {
            ValidateIfSessionAlreadyExists(objectToValidate);
        }

        private void ValidateIfSessionAlreadyExists(Session objectToValidate)
        {
            var alreadyInSession = Repository.Exists(objectToValidate);
            if (alreadyInSession)
            {
                var errorMessage = BusinessResource.ValidationError_AlreadyInSession;
                throw new ValidationException(errorMessage);
            }
        }
    }
}
