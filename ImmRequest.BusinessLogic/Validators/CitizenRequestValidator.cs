﻿using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Extensions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using ImmRequest.BusinessLogic.Resources;
using System.Net.Mail;

namespace ImmRequest.BusinessLogic.Validators
{
    public class CitizenRequestValidator : IValidator<CitizenRequest>
    {
        protected IRepository<Area> AreaRepository { get; set; }

        protected IRepository<Topic> TopicRepository { get; set; }

        protected IRepository<TopicType> TopicTypeRepository { get; set; }

        protected IRepository<BaseField> FieldsRepository { get; set; }

        protected CitizenRequestValidator() { }

        public CitizenRequestValidator(CitizenRequestValidatorInput repositories)
        {
            AreaRepository = repositories.AreaRepository;
            TopicRepository = repositories.TopicRepository;
            TopicTypeRepository = repositories.TopicTypeRepository;
            FieldsRepository = repositories.FieldRepository;
        }

        public bool IsValid(CitizenRequest objectToValidate)
        {
            throw new NotImplementedException();
        }

        protected bool AreBaseFieldValuesValid(ICollection<RequestFieldValues> requestFields)
        {
            foreach(var requestField in requestFields)
            {
                if (FieldExists(requestField.FieldId))
                {
                    var field = FieldsRepository.Get(requestField.FieldId);
                    bool isValid;
                    try
                    {
                        isValid = field.Validate(requestField.Value);
                    }
                    catch(Exception ex)
                    {
                        throw new ValidationException(ex.ToString());
                    }
                }
                else
                {
                    var errorMessage = string.Format(BusinessResource.ValidationError_FieldDoesntExists, requestField.FieldId);
                    throw new ValidationException(errorMessage);
                }
            }
            return true;
        }

        protected bool FieldExists(long fieldId)
        {
            var field = FieldsRepository.Get(fieldId);
            return field != null;
        }

        protected bool IsEmailValid(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                throw new ValidationException(BusinessResource.ValidationError_EmailIsInvalid);
            }
        }

        protected bool IsPhoneValid(string phone)
        {
            try
            {
                Convert.ToInt32(phone);
                return true;
            }
            catch (Exception)
            {
                throw new ValidationException(BusinessResource.ValidationError_EmailIsInvalid);
            }
        }
    }
}