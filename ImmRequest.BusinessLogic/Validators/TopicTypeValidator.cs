using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Validators
{
    public class TopicTypeValidator : IValidator<TopicType>
    {
        public bool IsValid(TopicType objectToValidate)
        {
            if (objectToValidate.ParentTopic == null)
            {
                throw new ValidationException("Every type must belong to a Topic");
            }

            if (string.IsNullOrEmpty(objectToValidate.Name))
            {
                throw new ValidationException("Must Contain a Name");
            }
            return true;
        }
    }
}
