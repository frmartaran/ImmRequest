﻿using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
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
            HasParentTopic(objectToValidate);
            HasName(objectToValidate);
            return true;
        }

        private static void HasName(TopicType objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.Name))
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_TopicType, BusinessResource.Field_Name);
                throw new ValidationException(message);
            }
        }

        private static void HasParentTopic(TopicType objectToValidate)
        {
            if (objectToValidate.ParentTopic == null)
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_TopicType, "a " + BusinessResource.Entity_Topic);
                throw new ValidationException(message);
            }
        }
    }
}