using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Exceptions;
using ImmRequest.WebApi.Helpers.Inputs;
using ImmRequest.WebApi.Interfaces;
using ImmRequest.WebApi.Models.UserManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ImmRequest.WebApi.Tests.ControllerTests
{
    [TestClass]
    public class SessionControllerTests
    {
        private SessionModel model;
        private Session session;

        [TestInitialize]
        public void SetUp()
        {
            model = new SessionModel
            {
                Email = "example@example.com",
                Password = "1234",
                Token = Guid.NewGuid()
            };

            session = new Session
            {
                AdministratorInSession = new Administrator
                {
                    Email = "another@example.com",
                    Password = "1235",
                    UserName = "Another Example"
                },
                Token = Guid.NewGuid()
            };

        }

        [TestMethod]
        public void ToDomainTest()
        {
            var newSession = model.ToDomain();
            Assert.AreEqual(model.Token, newSession.Token);
        }

        [TestMethod]
        public void ToDomainTestWithIDInModel()
        {
            model.Id = 2;
            var newSession = model.ToDomain();
            Assert.AreEqual(model.Token, newSession.Token);
            Assert.AreEqual(model.Id, newSession.Id);
        }

        [TestMethod]
        public void ToModelTest()
        {
            var newModel = SessionModel.ToModel(session);
            Assert.AreEqual(newModel.Token, session.Token);
            Assert.AreEqual(newModel.Email, session.AdministratorInSession.Email);
        }

        [TestMethod]
        public void Login()
        {
            var mockLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Create(It.IsAny<Session>()))
                .Returns(session.Token);
            mockLogic.Setup(m => m.Save());

            var mockAdministratorLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdministratorLogic.Setup(m => m.FindAdministratorByCredentials(
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Administrator());

            var mockHelper = new Mock<IContextHelper>();
            var inputs = new SessionControllerInputHelper(
                mockLogic.Object,
                mockAdministratorLogic.Object,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Login(model);

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            mockLogic.VerifyAll();
            mockAdministratorLogic.VerifyAll();
        }

        [TestMethod]
        public void LoginNullModel()
        {
            var mockSessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);

            var mockAdministratorLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);

            var mockHelper = new Mock<IContextHelper>();
            var inputs = new SessionControllerInputHelper(
                mockSessionLogic.Object,
                mockAdministratorLogic.Object,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Login(null);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            
        }

        [TestMethod]
        public void LoginWrongCredentials()
        {
            var mockSessionLogic = new Mock<ISessionLogic>();

            var mockAdministratorLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdministratorLogic.Setup(m => m.FindAdministratorByCredentials(
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns<Administrator>(null);

            var mockHelper = new Mock<IContextHelper>();
            var inputs = new SessionControllerInputHelper(
                mockSessionLogic.Object,
                mockAdministratorLogic.Object,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Login(model);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockSessionLogic.VerifyAll();
            mockAdministratorLogic.VerifyAll();
        }

        [TestMethod]
        public void LoginWithValidationErrorTest()
        {
            var mockSessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
            mockSessionLogic.Setup(m => m.Create(It.IsAny<Session>()))
                .Throws(new ValidationException(""));

            var mockAdministratorLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockAdministratorLogic.Setup(m => m.FindAdministratorByCredentials(
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Administrator());

            var mockHelper = new Mock<IContextHelper>();
            var inputs = new SessionControllerInputHelper(
                mockSessionLogic.Object,
                mockAdministratorLogic.Object,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Login(model);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockSessionLogic.VerifyAll();
            mockAdministratorLogic.VerifyAll();
        }

        [TestMethod]
        public void LogOutTest()
        {
            var mockLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(session);
            mockLogic.Setup(m => m.Delete(It.IsAny<long>()));
            mockLogic.Setup(m => m.Save());

            var mockAdministratorLogic = new Mock<IAdministratorLogic>().Object;

            var mockHelper = new Mock<IContextHelper>(MockBehavior.Strict);
            mockHelper.Setup(m => m.GetAuthorizationHeader(It.IsAny<HttpContext>()))
                .Returns(session.Token);

            var inputs = new SessionControllerInputHelper(
                mockLogic.Object,
                mockAdministratorLogic,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Logout();

            Assert.IsInstanceOfType(response, typeof(OkResult));
            mockLogic.VerifyAll();
            mockHelper.VerifyAll();

        }

        [TestMethod]
        public void LogOutSessionNotFoundGetCaseTest()
        {
            var mockSessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
            mockSessionLogic.Setup(m => m.Get(It.IsAny<Guid>()))
                .Throws(new BusinessLogicException(""));

            var mockAdministratorLogic = new Mock<IAdministratorLogic>().Object;

            var mockHelper = new Mock<IContextHelper>(MockBehavior.Strict);
            mockHelper.Setup(m => m.GetAuthorizationHeader(It.IsAny<HttpContext>()))
                .Returns(session.Token);

            var inputs = new SessionControllerInputHelper(
                mockSessionLogic.Object,
                mockAdministratorLogic,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Logout();

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockSessionLogic.VerifyAll();
            mockHelper.VerifyAll();

        }

        [TestMethod]
        public void LogOutSessionNotFoundDeleteCaseTest()
        {
            var mockSessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);
            mockSessionLogic.Setup(m => m.Get(It.IsAny<Guid>()))
                .Returns(session);
            mockSessionLogic.Setup(m => m.Delete(It.IsAny<long>()))
                .Throws(new BusinessLogicException("")); 

            var mockAdministratorLogic = new Mock<IAdministratorLogic>().Object;

            var mockHelper = new Mock<IContextHelper>(MockBehavior.Strict);
            mockHelper.Setup(m => m.GetAuthorizationHeader(It.IsAny<HttpContext>()))
                .Returns(session.Token);

            var inputs = new SessionControllerInputHelper(
                mockSessionLogic.Object,
                mockAdministratorLogic,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Logout();

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockSessionLogic.VerifyAll();
            mockHelper.VerifyAll();

        }

        [TestMethod]
        public void LogoutInvalidTokenTest()
        {
            var mockSessionLogic = new Mock<ISessionLogic>(MockBehavior.Strict);

            var mockAdministratorLogic = new Mock<IAdministratorLogic>().Object;

            var mockHelper = new Mock<IContextHelper>(MockBehavior.Strict);
            mockHelper.Setup(m => m.GetAuthorizationHeader(It.IsAny<HttpContext>()))
                .Throws(new HttpContextException(""));

            var inputs = new SessionControllerInputHelper(
                mockSessionLogic.Object,
                mockAdministratorLogic,
                mockHelper.Object
                );

            var controller = new SessionController(inputs);
            var response = controller.Logout();

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockSessionLogic.VerifyAll();
            mockHelper.VerifyAll();

        }

    }
}
