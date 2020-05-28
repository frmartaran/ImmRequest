using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
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
                Values = new List<string> { "Credencial" }
            };

            citizenRequest = new CitizenRequest
            {
                Id = 1,
                CitizenName = "Francisco",
                Description = "Quiero mi credencial!",
                Email = "immrequest@gmail.com",
                Phone = "21233457",
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
            CitizenRequestRepository = new CitizenRequestRepository(context);

            FieldRepository.Insert(additionalTextValues);
            TopicTypeRepository.Insert(topicType);
            TopicRepository.Insert(topic);
            AreaRepository.Insert(area);
            context.SaveChanges();
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

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            logic.Save();

            var citizenRequestCreated = context.CitizenRequests.FirstOrDefault();
            Assert.IsNotNull(citizenRequestCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvalidCitizenRequestTest()
        {
            CreateContextFor("Invalid citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            citizenRequest.AreaId = 15;
            logic.Create(citizenRequest);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvalidMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<CitizenRequest>()))
                .Throws(new ValidationException(""));
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(citizenRequest);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void GetCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(citizenRequest);
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Get(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void GetCitizenRequestTest()
        {
            CreateContextFor("Get citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            logic.Save();

            var requestReturned = logic.Get(citizenRequest.Id);
            Assert.AreEqual(requestReturned.Id, citizenRequest.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundTest()
        {
            CreateContextFor("Get not found citizen request");
            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Get(1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<CitizenRequest>(null);
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Get(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void GetAllCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.GetAll())
                .Returns(new List<CitizenRequest>
                {
                    citizenRequest
                }); ;
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.GetAll();

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void GetAllCitizenRequestTest()
        {
            CreateContextFor("Get all citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            logic.Save();
            var requestReturned = logic.GetAll()
                .FirstOrDefault();
            Assert.AreEqual(requestReturned.Id, citizenRequest.Id);
        }

        [TestMethod]
        public void UpdateValidCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Update(It.IsAny<CitizenRequest>()))
                .Returns(citizenRequest);
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<CitizenRequest>()))
                .Returns(true);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Update(citizenRequest);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void UpdateValidCitizenRequestTest()
        {
            CreateContextFor("Update valid citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            logic.Save();

            var requestCreated = logic.Get(citizenRequest.Id);
            requestCreated.CitizenName = "Paco";
            logic.Update(citizenRequest);
            logic.Save();

            var citizenRequestUpdated = logic.Get(citizenRequest.Id);
            Assert.IsNotNull(citizenRequest.CitizenName, citizenRequestUpdated.CitizenName);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void UpdateNotFoundTest()
        {
            CreateContextFor("Update not found citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            var requestCreated = logic.Get(citizenRequest.Id);
            requestCreated.CitizenName = "Paco";
            logic.Update(citizenRequest);
            var citizenRequestUpdated = logic.Get(citizenRequest.Id);
            Assert.IsNotNull(citizenRequest.CitizenName, citizenRequestUpdated.CitizenName);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void UpdateNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Update(It.IsAny<CitizenRequest>()))
                .Throws(new DatabaseNotFoundException(""));

            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<CitizenRequest>()))
                .Returns(true);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Update(citizenRequest);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateInvalidCitizenRequestTest()
        {
            CreateContextFor("Update invalid citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            logic.Save();

            var requestCreated = logic.Get(citizenRequest.Id);
            requestCreated.AreaId = 15;
            logic.Update(citizenRequest);
            logic.Save();

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateInvalidMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<CitizenRequest>()))
                .Throws(new ValidationException(""));
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Update(citizenRequest);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void DeleteCitizenRequestMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()));
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Delete(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void DeleteCitizenRequestTest()
        {
            CreateContextFor("Delete citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Create(citizenRequest);
            logic.Save();
            logic.Delete(citizenRequest.Id);
            logic.Save();

            var request = context.CitizenRequests.FirstOrDefault();
            Assert.IsNull(request);
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void DeleteNotFoundTest()
        {
            CreateContextFor("Delete not found citizen request");

            var citizenRequestRepository = new CitizenRequestRepository(context);
            var citizenRequestValidator = new CitizenRequestValidator(AreaRepository, TopicRepository, TopicTypeRepository, FieldRepository, CitizenRequestRepository);
            var logic = new CitizenRequestLogic(citizenRequestRepository, citizenRequestValidator);
            logic.Delete(citizenRequest.Id);
            var request = logic.Get(citizenRequest.Id);
            Assert.IsNull(request);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void DeleteNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<CitizenRequest>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()))
                .Throws(new DatabaseNotFoundException(""));
            var mockValidator = new Mock<IValidator<CitizenRequest>>(MockBehavior.Strict);
            var logic = new CitizenRequestLogic(mockRepository.Object, mockValidator.Object);
            logic.Delete(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }
    }
}
