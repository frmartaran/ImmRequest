using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class CitizenRequestValidatorTest : CitizenRequestValidator
    {
        public ImmDbContext context;

        public CitizenRequest citizenRequest;

        public TextField additionalTextValues;

        public TopicType topicType;

        public Topic topic;

        public Area area;

        public RequestFieldValues requestFieldValue;

        [TestInitialize]
        public void Setup()
        {
            additionalTextValues = new TextField
            {
                Id = 1,
                Name = "AdditionalTextValues",
                RangeValues = new List<string>
                {
                    "Credencial", "Cedula"
                },
                ParentTypeId = 1
            };
            topicType = new TopicType
            {
                Id = 1,
                AllFields = new List<BaseField>
                {
                    additionalTextValues
                },
                Name = "Renovacion",
                ParentTopicId = 1
            };
            topic = new Topic
            {
                Id = 1,
                Name = "TramitesLegales",
                Types = new List<TopicType>
                {
                    topicType
                },
                AreaId = 1
            };
            area = new Area
            {
                Id = 1,
                Name = "Area1",
                Topics = new List<Topic>
                {
                    topic
                }
            };
            requestFieldValue = new RequestFieldValues
            {
                Id = 1,
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Value = "Credencial"
            };

            citizenRequest = new CitizenRequest
            {
                Id = 1,
                CitizenName = "Francisco",
                Description = "Quiero mi credencial!",
                Email = "immrequest@gmail.com",
                Phone = "21233457",
                RequestNumber = 1,
                Status = Domain.Enums.RequestStatus.Created,
                AreaId = 1,
                TopicId = 1,
                TopicTypeId = 1,
                Values = new List<RequestFieldValues>
                {
                    requestFieldValue
                }
            };            
        }

        private void CreateContextFor(string contextName)
        {
            context = ContextFactory.GetMemoryContext(contextName);

            AreaRepository = new AreaRepository(context);
            TopicRepository = new TopicRepository(context);
            TopicTypeRepository = new TopicTypeRepository(context);
            FieldsRepository = new FieldsRepository(context);

            FieldsRepository.Insert(additionalTextValues);
            TopicTypeRepository.Insert(topicType);
            TopicRepository.Insert(topic);
            AreaRepository.Insert(area);
        }

        [TestMethod]
        public void FieldExists()
        {
            CreateContextFor("FieldExists");
            var exists = FieldExists(1);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void FieldDoesntExist()
        {
            CreateContextFor("FieldDoesntExist");
            var exists = FieldExists(2);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void BaseFieldsAreValid()
        {
            CreateContextFor("BaseFieldsAreValid");
            var isValid = AreBaseFieldValuesValid(citizenRequest.Values);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void BaseFieldsAreInvalid()
        {
            CreateContextFor("BaseFieldsAreInvalid");
            requestFieldValue.Value = "Panaderia";
            AreBaseFieldValuesValid(citizenRequest.Values);
        }

        [TestMethod]
        public void EmailIsValid()
        {
            CreateContextFor("EmailIsValid");
            var email = "a@a.com";
            var isValid = IsEmailValid(email);
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void EmailIsInvalid()
        {
            CreateContextFor("EmailIsInvalid");
            var email = "invalid";
            IsEmailValid(email);
        }

        [TestMethod]
        public void PhoneIsValid()
        {
            CreateContextFor("PhoneIsValid");
            var phone = "222222";
            var isValid = IsPhoneValid(phone);
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PhoneIsInvalid()
        {
            CreateContextFor("PhoneIsInvalid");
            var phone = "invalid";
            IsPhoneValid(phone);
        }
    }
}
