using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Validators
{
    public class TopicValidator : IValidator<Topic>
    {
        private IRepository<Topic> Repository { get; set; }

        public TopicValidator(IRepository<Topic> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Topic objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.Name))
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_Topic, BusinessResource.Field_Name);
                throw new ValidationException(message);
            }
            return true;
        }
    }
}
