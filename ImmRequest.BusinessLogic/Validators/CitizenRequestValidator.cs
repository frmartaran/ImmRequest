using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using System;
using System.Collections.Generic;
using ImmRequest.BusinessLogic.Resources;
using System.Net.Mail;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Enums;
using ImmRequest.BusinessLogic.Helpers;
using System.Linq;
using ImmRequest.BusinessLogic.Helpers.Inputs;

namespace ImmRequest.BusinessLogic.Validators
{
    public class CitizenRequestValidator : IValidator<CitizenRequest>
    {
        protected IRepository<Area> AreaRepository { get; set; }

        protected IRepository<Topic> TopicRepository { get; set; }

        protected IRepository<TopicType> TopicTypeRepository { get; set; }

        protected IRepository<BaseField> FieldRepository { get; set; }

        protected IRepository<CitizenRequest> CitizenRequestRepository { get; set; }

        protected CitizenRequestValidator() { }

        public CitizenRequestValidator(CitizenRequestValidatorInput input)
        {
            AreaRepository = input.AreaRepository;
            TopicRepository = input.TopicRepository;
            TopicTypeRepository = input.TopicTypeRepository;
            FieldRepository = input.FieldRepository;
            CitizenRequestRepository = input.CitizenRequestRepository;
        }

        public bool IsValid(CitizenRequest objectToValidate)
        {
            IsCitizenNameValid(objectToValidate.CitizenName);
            IsDescriptionValid(objectToValidate.Description);
            IsEmailValid(objectToValidate.Email);
            IsRequestStatusValid(objectToValidate);
            IsAreaValid(objectToValidate.AreaId);
            IsTopicValid(objectToValidate.AreaId, objectToValidate.TopicId);
            IsTopicTypeValid(objectToValidate.AreaId, objectToValidate.TopicId, objectToValidate.TopicTypeId);
            AreBaseFieldValuesValid(objectToValidate.Values);
            AllFieldsHaveValues(objectToValidate.TopicTypeId, objectToValidate.Values);
            return true;
        }

        protected bool IsRequestStatusValid(CitizenRequest objectToValidate)
        {
            var existingRequest = CitizenRequestRepository.Get(objectToValidate.Id);
            if (existingRequest != null)
            {
                if (!StatusUpdatedIsValid(existingRequest.Status, objectToValidate.Status))
                {
                    var exception = string.Format(BusinessResource.ValidationError_UpdateRequestStatusIsInvalid,
                        existingRequest.Status.ToString(), objectToValidate.Status.ToString());
                    throw new ValidationException(exception);
                }
            }
            else
            {
                if (objectToValidate.Status != RequestStatus.Created)
                {
                    throw new ValidationException(BusinessResource.ValidationError_CreateRequestStatusIsInvalid);
                }
            }
            return true;
        }

        protected bool StatusUpdatedIsValid(RequestStatus oldStatus, RequestStatus newStatus)
        {
            return oldStatus == newStatus
                || StatusHelper.NextStatuses(oldStatus).Contains(newStatus)
                || StatusHelper.PreviousStatuses(oldStatus).Contains(newStatus);
        }

        protected bool AreBaseFieldValuesValid(ICollection<RequestFieldValues> requestFields)
        {
            AreFieldValuesValid(requestFields);
            return true;

        }

        private void AreFieldValuesValid(ICollection<RequestFieldValues> requestFields)
        {
            foreach (var requestField in requestFields)
            {
                if (FieldExists(requestField.FieldId))
                {
                    var field = FieldRepository.Get(requestField.FieldId);
                    bool isValid;
                    try
                    {
                        isValid = field.Validate(requestField.Values);
                    }
                    catch (DomainValidationException exception)
                    {
                        throw new ValidationException(exception.Message);
                    }
                    catch (InvalidArgumentException exception)
                    {
                        throw new ValidationException(exception.Message);

                    }
                }
                else
                {
                    var errorMessage = string.Format(BusinessResource.ValidationError_FieldDoesntExists,
                        requestField.FieldId);
                    throw new ValidationException(errorMessage);
                }
            }
        }

        protected bool FieldExists(long fieldId)
        {
            var field = FieldRepository.Get(fieldId);
            return field != null;
        }

        protected bool IsCitizenNameValid(string citizenName)
        {
            if (string.IsNullOrEmpty(citizenName))
            {
                throw new ValidationException(BusinessResource.ValidationError_CitizenNameIsInvalid);
            }
            return true;
        }

        protected bool IsDescriptionValid(string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                throw new ValidationException(BusinessResource.ValidationError_DescriptionIsInvalid);
            }
            return true;
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

        protected bool IsAreaValid(long areaId)
        {
            HasArea(areaId);
            return true;
        }

        private void HasArea(long areaId)
        {
            var area = AreaRepository.Get(areaId);
            if (area == null)
                throw new ValidationException(BusinessResource.ValidationError_AreaIsInvalid);
        }

        protected bool IsTopicValid(long areaId, long topicId)
        {
            var topic = TopicRepository.Get(topicId);
            HasTopic(topic);
            DoesTopicBelongsToSelectedArea(areaId, topic);
            return true;
        }

        private static void HasTopic(Topic topic)
        {
            if (topic == null)
                throw new ValidationException(BusinessResource.ValidationError_TopicIsInvalid);
        }

        private static void DoesTopicBelongsToSelectedArea(long areaId, Topic topic)
        {
            if (topic.AreaId != areaId)
            {
                var message = string.Format(BusinessResource.ValidationError_MustBelong,
                    BusinessResource.Entity_Topic, BusinessResource.Entity_Area);
                throw new ValidationException(message);
            }
        }

        protected bool IsTopicTypeValid(long areaId, long topicId, long topicTypeId)
        {
            var topicType = TopicTypeRepository.Get(topicTypeId);
            HasType(topicType);
            DoesTypeBelongToSelectedTopic(topicId, topicType);
            return true;
        }

        private static void DoesTypeBelongToSelectedTopic(long topicId, TopicType topicType)
        {
            if (topicType.ParentTopicId != topicId)
            {
                var message = string.Format(BusinessResource.ValidationError_MustBelong,
                   BusinessResource.Entity_TopicType, BusinessResource.Entity_Topic);
                throw new ValidationException(message);
            }
        }

        private static void HasType(TopicType topicType)
        {
            if (topicType == null)
                throw new ValidationException(BusinessResource.ValidationError_TopicTypeIsInvalid);
        }

        protected bool AllFieldsHaveValues(long typeId, List<RequestFieldValues> values)
        {
            if (values.FirstOrDefault()?.ParentCitizenRequestId != 0)
                return true;

            var type = TopicTypeRepository.Get(typeId);
            var fieldsIds = type.AllFields.Select(f => f.Id).ToList();
            var valuesFieldsIds = values.Select(rfv => rfv.FieldId).ToList();
            var intersection = fieldsIds.Intersect(valuesFieldsIds).Count();
            if (intersection != fieldsIds.Count)
            {
                var message = string.Format(BusinessResource.ValidationError_MustContainField,
                    BusinessResource.Entity_Field, BusinessResource.OneOrMore_Value);
                throw new ValidationException(message);

            }

            return true;

        }
    }
}