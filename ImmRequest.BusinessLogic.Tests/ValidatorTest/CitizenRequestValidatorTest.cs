using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Exceptions;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class CitizenRequestValidatorTest : CitizenRequestValidator
    {
        public ImmDbContext context;

        public CitizenRequest citizenRequest;

        public TextField additionalTextValues;

        public NumberField numberField;

        public TopicType topicType;

        public Topic topic;

        public Area area;

        public RequestFieldValues requestFieldValue;

        [TestInitialize]
        public void Setup()
        {
            additionalTextValues = new TextField
            {
                Name = "AdditionalTextValues",
                RangeValues = new List<string>
                {
                    "Credencial", "Cedula"
                },
                ParentTypeId = 1
            };
            numberField = new NumberField
            {
                RangeStart = 1,
                RangeEnd = 10,
                Name = "Age Range"

            };
            topicType = new TopicType
            {
                AllFields = new List<BaseField>
                {
                    additionalTextValues,
                    numberField
                },
                Name = "Renovacion",
                ParentTopicId = 1
            };
            topic = new Topic
            {
                Name = "TramitesLegales",
                Types = new List<TopicType>
                {
                    topicType
                },
                AreaId = 1
            };
            area = new Area
            {
                Name = "Area1",
                Topics = new List<Topic>
                {
                    topic
                }
            };
            requestFieldValue = new RequestFieldValues
            {
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Value = "Credencial"
            };

            citizenRequest = new CitizenRequest
            {
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
            FieldRepository = new FieldsRepository(context);

            FieldRepository.Insert(additionalTextValues);
            TopicTypeRepository.Insert(topicType);
            TopicRepository.Insert(topic);
            AreaRepository.Insert(area);
        }

        [TestMethod]
        public void FieldExistsTest()
        {
            CreateContextFor("FieldExists");
            var exists = FieldExists(1);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void FieldExitstMockTest()
        {
            var mockFieldRepository = new Mock<IRepository<BaseField>>(MockBehavior.Strict);
            mockFieldRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(additionalTextValues);
            FieldRepository = mockFieldRepository.Object;
            var exitst = FieldExists(1);
            mockFieldRepository.VerifyAll();
            Assert.IsTrue(exitst);
        }

        [TestMethod]
        public void FieldDoesntExist()
        {
            CreateContextFor("FieldDoesntExist");
            var exists = FieldExists(3);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void FieldDoesNotExitstMockTest()
        {
            var mockFieldRepository = new Mock<IRepository<BaseField>>(MockBehavior.Strict);
            mockFieldRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<BaseField>(null);
            FieldRepository = mockFieldRepository.Object;
            var exitst = FieldExists(1);
            mockFieldRepository.VerifyAll();
            Assert.IsFalse(exitst);
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
        [ExpectedException(typeof(ValidationException))]
        public void BaseFieldHaveInvalidFormat()
        {
            CreateContextFor("BaseFieldsAreInvalid");
            requestFieldValue.Value = "Panaderia";
            requestFieldValue.FieldId = 2;
            AreBaseFieldValuesValid(citizenRequest.Values);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void BaseFieldDoesntExistsInDbAnymoreTest()
        {
            CreateContextFor("BaseFieldsAreInvalid");
            context.Fields.Remove(numberField);
            context.SaveChanges();
            requestFieldValue.FieldId = 2;
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

        [TestMethod]
        public void AreaIsValid()
        {
            CreateContextFor("AreaIsValid");
            var isValid = IsAreaValid(citizenRequest.AreaId);
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void AreaIsInvalid()
        {
            CreateContextFor("AreaIsInvalid");
            IsAreaValid(150);
        }

        [TestMethod]
        public void TopicIsValid()
        {
            CreateContextFor("TopicIsValid");
            var isValid = IsTopicValid(citizenRequest.AreaId, citizenRequest.TopicId);
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TopicIsInvalid()
        {
            CreateContextFor("TopicIsInvalid");
            IsTopicValid(citizenRequest.AreaId, 150);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TopicIsInvalidTopicDoesNotBelongToParentAreaTest()
        {
            CreateContextFor("TopicIsInvalid");
            IsTopicValid(2, 1);
        }

        [TestMethod]
        public void TopicTypeIsValid()
        {
            CreateContextFor("TopicTypeIsValid");
            var isValid = IsTopicTypeValid(citizenRequest.AreaId, citizenRequest.TopicId, citizenRequest.TopicTypeId);
            Assert.IsTrue(isValid);
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TopicTypeIsInvalid()
        {
            CreateContextFor("TopicTypeIsInvalid");
            IsTopicTypeValid(citizenRequest.AreaId, citizenRequest.TopicId, 150);
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void TopicTypeIsInvalidTopicSelectedIsNotParentTest()
        {
            CreateContextFor("TopicTypeIsInvalid");
            IsTopicTypeValid(citizenRequest.AreaId, 3, 1);
        }
    }
}
