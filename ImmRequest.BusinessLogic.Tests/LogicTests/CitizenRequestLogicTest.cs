using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Extensions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.BusinessLogic.Tests.LogicTests
{
    [TestClass]
    public class CitizenRequestLogicTest
    {
        private ImmDbContext context;

        private CitizenRequest citizenRequest;

        private TextField additionalTextValues;

        private TopicType topicType;

        private Topic topic;

        private Area area;

        private RequestFieldValues requestFieldValue;

        private CitizenRequestValidatorInput validatorInput;

        private IRepository<Area> AreaRepository;

        private IRepository<Topic> TopicRepository;

        private IRepository<TopicType> TopicTypeRepository;

        private IRepository<BaseField> FieldRepository;

        private IRepository<CitizenRequest> CitizenRequestRepository;

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
            FieldRepository = new FieldsRepository(context);

            FieldRepository.Insert(additionalTextValues);
            TopicTypeRepository.Insert(topicType);
            TopicRepository.Insert(topic);
            AreaRepository.Insert(area);
        }

        [TestMethod]
        public void CreateValidCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<CitizenRequest>()));
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<CitizenRequest>()))
                .Returns(true);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(citizenRequest);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void CreateValidCitizenRequestTest()
        {
            CreateContextFor("Valid citizen request");

            var validatorInput = new CitizenRequestValidatorInput
            {
                AreaRepository = AreaRepository,
                FieldRepository = FieldRepository,
                TopicRepository = TopicRepository,
                TopicTypeRepository = TopicTypeRepository
            };
            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(validatorInput);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);

            var citizenRequestCreated = context.CitizenRequests.FirstOrDefault();
            Assert.IsNotNull(citizenRequestCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvalidCitizenRequestTest()
        {
            CreateContextFor("Valid citizen request");

            var validatorInput = new CitizenRequestValidatorInput
            {
                AreaRepository = AreaRepository,
                FieldRepository = FieldRepository,
                TopicRepository = TopicRepository,
                TopicTypeRepository = TopicTypeRepository
            };
            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(validatorInput);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            citizenRequest.AreaId = 15;
            logic.Create(citizenRequest);
        }

        [TestMethod]
        public void GetCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()));
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<CitizenRequest>()))
                .Returns(true);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Get(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void GetCitizenRequestTest()
        {
            CreateContextFor("Get citizen request");

            var validatorInput = new CitizenRequestValidatorInput
            {
                AreaRepository = AreaRepository,
                FieldRepository = FieldRepository,
                TopicRepository = TopicRepository,
                TopicTypeRepository = TopicTypeRepository
            };
            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(validatorInput);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            var requestReturned = logic.Get(citizenRequest.Id);
            Assert.AreEqual(requestReturned.Id, citizenRequest.Id);
        }
    }
}
