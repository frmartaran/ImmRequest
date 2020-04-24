using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Resources;
using ImmRequest.Domain;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Fields;
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
            AreCustomFieldsValid(objectToValidate);
            return true;
        }

        private static void AreCustomFieldsValid(TopicType objectToValidate)
        {
            foreach (var field in objectToValidate.AllFields)
            {
                CustomFieldHasName(field);
                HasValidRange(field);
            }
        }

        private static void HasValidRange(BaseField field)
        {
            try
            {
                field.ValidateRangeValues();
            }
            catch (DomainValidationException exception)
            {
                throw new ValidationException(exception.Message);
            }
        }

        private static void CustomFieldHasName(BaseField field)
        {
            if (string.IsNullOrEmpty(field.Name))
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_TopicType, BusinessResource.Field_Name);
                throw new ValidationException(message);
            }
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
