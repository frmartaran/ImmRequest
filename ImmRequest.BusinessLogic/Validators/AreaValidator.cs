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
    public class AreaValidator : IValidator<Area>
    {
        private IRepository<Area> Repository { get; set; }

        public AreaValidator(IRepository<Area> repository)
        {
            Repository = repository;
        }
        public bool IsValid(Area objectToValidate)
        {
            HasName(objectToValidate);
            HasUniqueName(objectToValidate);
            HasTypes(objectToValidate);
            return true;
        }

        private static void HasTypes(Area objectToValidate)
        {
            if (objectToValidate.Topics.Count == 0)
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_Area, "at least one " + BusinessResource.Entity_Topic);
                throw new ValidationException(message);
            }
        }

        private void HasUniqueName(Area objectToValidate)
        {
            var areaExists = Repository.Exists(objectToValidate);
            if (areaExists)
            {
                var message = string.Format(BusinessResource.ValidationError_MustBeUnique,
                    BusinessResource.Field_Name);
                throw new ValidationException(message);
            }
        }

        private static void HasName(Area objectToValidate)
        {
            if (string.IsNullOrEmpty(objectToValidate.Name))
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_Area, BusinessResource.Field_Name);
                throw new ValidationException(message);
            }
        }
    }
}
