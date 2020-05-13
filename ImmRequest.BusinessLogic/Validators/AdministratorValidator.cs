using System;
using System.Net.Mail;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain.UserManagement;

namespace ImmRequest.BusinessLogic.Validators
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
            ValidateUsername(objectToValidate);
            ValidateEmail(objectToValidate);
            ValidatePassword(objectToValidate);

            return true;
        }

        private static void ValidatePassword(Administrator objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.PassWord))
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_IsEmpty,
                    Password_PropertyName);
                throw new ValidationException(errorMessage);
            }
        }

        private void ValidateEmail(Administrator objectToValidate)
        {
            ValidateEmailsIsNotEmpty(objectToValidate);
            ValidateEmailFormat(objectToValidate);
            ValidateEmailIsUnique(objectToValidate);
        }

        private static void ValidateEmailFormat(Administrator objectToValidate)
        {
            try
            {
                new MailAddress(objectToValidate.Email);
            }
            catch (FormatException)
            {
                throw new ValidationException(BusinessResource.ValidationError_EmailIsInvalid);

            }
        }

        private void ValidateEmailIsUnique(Administrator objectToValidate)
        {
            var adminWithEmailExits = Repository.Exists(objectToValidate);
            if (adminWithEmailExits)
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_MustBeUnique,
                    Email_PropertyName);
                throw new ValidationException(errorMessage);
            }
        }

        private static void ValidateEmailsIsNotEmpty(Administrator objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.Email))
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_IsEmpty,
                    Email_PropertyName);
                throw new ValidationException(errorMessage);
            }
        }

        private static void ValidateUsername(Administrator objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.UserName))
            {
                var errorMessage = string.Format(BusinessResource.ValidationError_IsEmpty,
                    UserName_PropertyName);
                throw new ValidationException(errorMessage);
            }
        }
    }
}
