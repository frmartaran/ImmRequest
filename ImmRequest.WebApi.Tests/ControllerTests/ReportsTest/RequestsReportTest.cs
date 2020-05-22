using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Helpers;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Models;
using ImmRequest.WebApi.Models.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.WebApi.Tests.ControllerTests.ReportsTest
{
    [TestClass]
    public class RequestsReportTest
    {
        string citizenEmail;
        CitizenRequest FirstRequest;
        CitizenRequest SecondRequest;
        CitizenRequest ThirdRequest;
        CitizenRequest FourthRequest;
        CitizenRequest FifthRequest;
        CitizenRequest SixthRequest;
        List<CitizenRequest> AllRequests;
        Type okType;
        Type BadRequest;

        [TestInitialize]
        public void SetUp()
        {
            okType = typeof(OkObjectResult);
            BadRequest = typeof(BadRequestObjectResult);

            citizenEmail = "user@email.com";
            var startDate = new DateTime(2020, 1, 1);
            FirstRequest = new CitizenRequest
            {
                CreatedDate = startDate,
                Email = citizenEmail,
                Status = RequestStatus.Created,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType()
            };

            SecondRequest = new CitizenRequest
            {
                CreatedDate = startDate.AddDays(1),
                Email = citizenEmail,
                Status = RequestStatus.Created,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType()
            };

            ThirdRequest = new CitizenRequest
            {
                CreatedDate = startDate.AddDays(2),
                Email = citizenEmail,
                Status = RequestStatus.OnRevision,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType()
            };

            FourthRequest = new CitizenRequest
            {
                CreatedDate = startDate.AddDays(3),
                Email = citizenEmail,
                Status = RequestStatus.OnRevision,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType()
            };

            FifthRequest = new CitizenRequest
            {
                CreatedDate = startDate.AddDays(4),
                Email = citizenEmail,
                Status = RequestStatus.Acepted,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType()
            };

            SixthRequest = new CitizenRequest
            {
                CreatedDate = startDate.AddDays(3),
                Email = "another@email.com",
                Status = RequestStatus.Created,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType()
            };

            AllRequests = new List<CitizenRequest>
            {
                FirstRequest,
                SecondRequest,
                ThirdRequest,
                FourthRequest,
                FifthRequest,
                SixthRequest,
            };

        }

        [TestMethod]
        public void GetSummaryReport()
        {
            
            var finderMock = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            finderMock.Setup(m => m.GetAll())
                .Returns(AllRequests);

            var controller = new ReportsController(finderMock.Object);
            var model = new RequestSummaryReportModel
            {
                Email = citizenEmail,
                Start = new DateTime(2020, 1, 1),
                End = new DateTime(2020, 2, 1)
            };

            var response = controller.RequestSummaryReportGet(model);
            Assert.IsInstanceOfType(response, okType);

            var asOkReponse = response as OkObjectResult;
            var content = asOkReponse.Value as RequestSummaryReportModel;
            var createdRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Created);
            var onRevisionRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.OnRevision);
            var acceptedRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Acepted);
            var declinedRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Declined);
            var finishedRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Ended);

            Assert.AreEqual(2, createdRequests.Count);
            Assert.AreEqual(2, onRevisionRequests.Count);
            Assert.AreEqual(1, acceptedRequests.Count);
            Assert.IsNull(declinedRequests);
            Assert.IsNull(finishedRequests);
        }

        [TestMethod]
        public void GetSummaryReportNoMock()
        {
            var context = ContextFactory.GetMemoryContext("Request Summary Report");
            context.CitizenRequests.AddRange(AllRequests);
            context.SaveChanges();

            var repository = new CitizenRequestRepository(context);
            var validatorMock = new Mock<IValidator<CitizenRequest>>();
            var logic = new CitizenRequestLogic(repository, validatorMock.Object);

            var controller = new ReportsController(logic);
            var model = new RequestSummaryReportModel
            {
                Email = citizenEmail,
                Start = new DateTime(2020, 1, 1),
                End = new DateTime(2020, 2, 1)
            };

            var response = controller.RequestSummaryReportGet(model);
            Assert.IsInstanceOfType(response, okType);

            var asOkReponse = response as OkObjectResult;
            var content = asOkReponse.Value as RequestSummaryReportModel;
            var createdRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Created);
            var onRevisionRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.OnRevision);
            var acceptedRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Acepted);
            var declinedRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Declined);
            var finishedRequests = content.Summary
                .FirstOrDefault(x => x.Status == RequestStatus.Ended);

            Assert.AreEqual(2, createdRequests.Count);
            Assert.AreEqual(2, onRevisionRequests.Count);
            Assert.AreEqual(1, acceptedRequests.Count);
            Assert.IsNull(declinedRequests);
            Assert.IsNull(finishedRequests);
        }


        [TestMethod]
        public void NoRequestDuringThatDateTest()
        {
            var context = ContextFactory.GetMemoryContext("No Requests");
            context.CitizenRequests.AddRange(AllRequests);
            context.SaveChanges();

            var repository = new CitizenRequestRepository(context);
            var validatorMock = new Mock<IValidator<CitizenRequest>>();
            var logic = new CitizenRequestLogic(repository, validatorMock.Object);

            var controller = new ReportsController(logic);
            var model = new RequestSummaryReportModel
            {
                Email = citizenEmail,
                Start = new DateTime(2021, 1, 1),
                End = new DateTime(2021, 2, 1)
            };

            var response = controller.RequestSummaryReportGet(model);
            context.Dispose();
            Assert.IsInstanceOfType(response, BadRequest);

        }

        [TestMethod]
        public void NoRequestForEmailTest()
        {
            var finderMock = new Mock<ILogic<CitizenRequest>>(MockBehavior.Strict);
            finderMock.Setup(m => m.GetAll())
                .Returns(AllRequests);

            var controller = new ReportsController(finderMock.Object);
            var model = new RequestSummaryReportModel
            {
                Email = "some@user.com",
                Start = new DateTime(2020, 1, 1),
                End = new DateTime(2020, 2, 1)
            };

            var response = controller.RequestSummaryReportGet(model);
            Assert.IsInstanceOfType(response, BadRequest);
        }
    }

}

