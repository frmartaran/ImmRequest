using ImmRequest.BusinessLogic.Exceptions;
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

        protected IRepository<BaseField> FieldRepository { get; set; }

        protected CitizenRequestValidator() { }

        public CitizenRequestValidator(CitizenRequestValidatorInput repositories)
        {
            AreaRepository = repositories.AreaRepository;
            TopicRepository = repositories.TopicRepository;
            TopicTypeRepository = repositories.TopicTypeRepository;
            FieldRepository = repositories.FieldRepository;
        }

        public bool IsValid(CitizenRequest objectToValidate)
        {
            IsEmailValid(objectToValidate.Email);
            IsPhoneValid(objectToValidate.Phone);
            IsAreaValid(objectToValidate.AreaId);
            IsTopicValid(objectToValidate.AreaId, objectToValidate.TopicId);
            IsTopicTypeValid(objectToValidate.AreaId, objectToValidate.TopicId, objectToValidate.TopicTypeId);
            AreBaseFieldValuesValid(objectToValidate.Values);
            return true;
        }

        protected bool AreBaseFieldValuesValid(ICollection<RequestFieldValues> requestFields)
        {
            foreach(var requestField in requestFields)
            {
                if (FieldExists(requestField.FieldId))
                {
                    var field = FieldRepository.Get(requestField.FieldId);
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
            var field = FieldRepository.Get(fieldId);
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

        protected bool IsAreaValid(long areaId)
        {
            var area = AreaRepository.Get(areaId);
            if (area == null)
                throw new ValidationException(BusinessResource.ValidationError_AreaIsInvalid);
            return true;
        }

        protected bool IsTopicValid(long areaId, long topicId)
        {
            var topic = TopicRepository.Get(topicId);
            if(topic == null || topic.AreaId != areaId)
                throw new ValidationException(BusinessResource.ValidationError_TopicIsInvalid);
            return true;
        }

        protected bool IsTopicTypeValid(long areaId, long topicId, long topicTypeId)
        {
            var topicType = TopicTypeRepository.Get(topicTypeId);
            if (topicType == null || topicType.ParentTopicId != topicId || topicType.ParentTopic.AreaId != areaId)
                throw new ValidationException(BusinessResource.ValidationError_TopicTypeIsInvalid);
            return true;
        }
    }
}