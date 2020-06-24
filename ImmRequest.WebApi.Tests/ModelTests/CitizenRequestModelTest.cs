using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImmRequest.WebApi.Tests.ModelTests
{
    [TestClass]
    public class CitizenRequestModelTest
    {
        private CitizenRequest request;

        private RequestFieldValues requestFieldValue;

        [TestInitialize]
        public void Setup()
        {
            requestFieldValue = new RequestFieldValues
            {
                Id = 1,
                ParentCitizenRequestId = 1,
                FieldId = 1,
                Field = new TextField
                {
                    Name = "Documentos"
                },
                Values = new List<string> { "Credencial" }
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
                Area = new Area { Name = "Area" },
                Topic = new Topic { Name = "Topic" },
                TopicType = new TopicType { Name = "Type" },
                TopicId = 1,
                TopicTypeId = 1,
                Values = new List<RequestFieldValues>
                {
                    requestFieldValue
                }
            };
        }

        [TestMethod]
        public void SetModel()
        {
            var requestModel = new CitizenRequestModel();

            requestModel.SetModel(request);

            Assert.AreEqual(request.Id, requestModel.Id);
            Assert.AreEqual(request.CitizenName, requestModel.CitizenName);
            Assert.AreEqual(request.Description, requestModel.Description);
            Assert.AreEqual(request.Email, requestModel.Email);
            Assert.AreEqual(request.Phone, requestModel.Phone);
            Assert.AreEqual(request.Status, requestModel.Status);
            Assert.AreEqual(request.AreaId, requestModel.AreaId);
            Assert.AreEqual(request.TopicId, requestModel.TopicId);
            Assert.AreEqual(request.TopicTypeId, requestModel.TopicTypeId);
            Assert.AreEqual(request.Values.FirstOrDefault().Id, requestModel.Values.FirstOrDefault().Id);
        }

        [TestMethod]
        public void ToDomain()
        {
            var requestFieldValuesModel = RequestFieldValuesModel.ToModel
            (
                new List<RequestFieldValues>
                {
                    requestFieldValue
                }
            )
            .ToList();
            var requestModel = new CitizenRequestModel
            {
                CitizenName = "Francisco",
                Description = "Quiero mi credencial!",
                Email = "immrequest@gmail.com",
                Phone = "21233457",
                AreaId = 1,
                TopicId = 1,
                TopicTypeId = 1,
                Values = requestFieldValuesModel
            };

            var request = requestModel.ToDomain();

            Assert.AreEqual(requestModel.CitizenName, request.CitizenName);
            Assert.AreEqual(requestModel.Description, request.Description);
            Assert.AreEqual(requestModel.Email, request.Email);
            Assert.AreEqual(requestModel.Phone, request.Phone);
            Assert.AreEqual(requestModel.AreaId, request.AreaId);
            Assert.AreEqual(requestModel.TopicId, request.TopicId);
            Assert.AreEqual(requestModel.TopicTypeId, request.TopicTypeId);
            Assert.AreEqual(requestModel.Values.FirstOrDefault().Id, request.Values.FirstOrDefault().Id);
        }
    }
}
