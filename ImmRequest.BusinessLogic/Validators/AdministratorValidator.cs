using System;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;

namespace ImmRequest.BusinessLogic
{
    public class AdministratorValidator : IValidator<Administrator>
    {
        private IRepository<Administrator> Repository { get; set; }

        private const string UserName_PropertyName = "Username";
        private const string Email_PropertyName = "Email";
        private const string Password_PropertyName = "Password";

        public AdministratorValidator(IRepository<Administrator> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Administrator objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.UserName))
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_IsEmpty, 
                    UserName_PropertyName);
                throw new ValidationException(errorMessage);
            }
            var adminWithEmailExits = Repository.Exists(objectToValidate);
            if (adminWithEmailExits)
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_MustBeUnique,
                    Email_PropertyName);
                throw new ValidationException(errorMessage);
            }

            return true;
        }
    }
}
