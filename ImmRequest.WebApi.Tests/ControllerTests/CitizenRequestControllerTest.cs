using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.WebApi.Tests.ControllerTests
{
    [TestClass]
    public class CitizenRequestControllerTest
    {
        private CitizenRequest request;

        private CitizenRequestModel requestModel;

        private RequestFieldValues requestFieldValues;

        private RequestFieldValuesModel requestFieldValuesModel;

        private Area area;

        private Topic topic;

        [TestInitialize]
        public void SetUp()
        {
            requestFieldValues = new RequestFieldValues
            {
                Id = 1,
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Value = "Credencial"
            };
            requestFieldValuesModel = new RequestFieldValuesModel
            {
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Value = "Credencial"
            };
            request = new CitizenRequest
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
                    requestFieldValues
                }
            };
            requestModel = new CitizenRequestModel
            {
                CitizenName = "Francisco",
                Description = "Quiero mi credencial!",
                Email = "immrequest@gmail.com",
                Phone = "21233457",
                Status = Domain.Enums.RequestStatus.Created,
                AreaId = 1,
                TopicId = 1,
                TopicTypeId = 1,
                Values = new List<RequestFieldValuesModel>
                {
                    requestFieldValuesModel
                }
            };
            var topicType = new TopicType
            {
                Id = 1,
                AllFields = new List<BaseField>(),
                Name = "Francisco's type",
                ParentTopicId = 1
            };
            var topicTypes = new List<TopicType>
            {
                topicType
            };
            var topic = new Topic
            {
                Id = 1,
                AreaId = 1,
                Name = "Francisco's topic",
                Types = topicTypes
            };
            area = new Area
            {
                Id = 1,
                Name = "Francisco's Area",
                Topics = new List<Topic>
                {
                    topic
                }
            };
        }

        [TestMethod]
        public void CreateCitizenRequest()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Create(It.IsAny<CitizenRequest>()));

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.CreateCitizenRequest(requestModel);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void CreateCitizenRequestNullBody()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.CreateCitizenRequest(null);

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void CreateCitizenRequestValidationError()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Create(It.IsAny<CitizenRequest>()))
                .Throws(new ValidationException(""));

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.CreateCitizenRequest(requestModel);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetCitizenRequest()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(request);

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetCitizenRequest(1);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetCitizenRequestNotFoundRequest()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Throws(new BusinessLogicException(""));

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetCitizenRequest(5);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetCitizenRequestStatus()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(request);

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetCitizenRequestStatus(1);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetCitizenRequestStatusNotFoundRequest()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Throws(new BusinessLogicException(""));

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetCitizenRequestStatus(5);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetCitizenRequests()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.GetAll())
                .Returns(new List<CitizenRequest> 
                {
                    request
                });

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetCitizenRequests();

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void UpdateCitizenRequestStatus()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(request);

            mockCitizenRequestLogic.Setup(m => m.Update(It.IsAny<CitizenRequest>()));

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.UpdateCitizenRequestStatus(1, Domain.Enums.RequestStatus.Created);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void UpdateCitizenRequestStatusUpdateError()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockCitizenRequestLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(request);

            mockCitizenRequestLogic.Setup(m => m.Update(It.IsAny<CitizenRequest>()))
                .Throws(new BusinessLogicException(""));

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.UpdateCitizenRequestStatus(1, Domain.Enums.RequestStatus.Created);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public void GetAllAreas()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockAreaFinder.Setup(m => m.FindAll())
                .Returns(new List<Area> { area });

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetAllAreas();

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAllTopicsFromArea()
        {
            var mockCitizenRequestLogic = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            var mockTopicFinder = new Mock<IFinder<Topic>>(MockBehavior.Strict);
            var mockAreaFinder = new Mock<IFinder<Area>>(MockBehavior.Strict);

            mockTopicFinder.Setup(m => m.FindAll(It.IsAny<Predicate<Topic>>()))
                .Returns(new List<Topic> { topic });

            var controller = new CitizenRequestController(mockCitizenRequestLogic.Object, mockTopicFinder.Object, mockAreaFinder.Object);
            var result = controller.GetAllTopicsFromArea(1);

            mockCitizenRequestLogic.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}
