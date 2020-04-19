using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

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
                    PassWord = "1235",
                    UserName = "Another Example"
                },
                Token = Guid.NewGuid()
            };

        }

        [TestMethod]
        public void ToDomainTest()
        {
            var newSession = model.ToDomain();
            Assert.AreEqual(model.Email, newSession.AdministratorInSession.Email);
            Assert.AreEqual(model.Token, newSession.Token);
        }

        [TestMethod]
        public void ToDomainTestWithIDInModel()
        {
            model.Id = 2;
            var newSession = model.ToDomain();
            Assert.AreEqual(model.Email, newSession.AdministratorInSession.Email);
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
            var mockSessionLogic = new Mock<ISessionLogic>();
            mockSessionLogic.Setup(m => m.Create(It.IsAny<Session>()));

            var mockAdministratorLogic = new Mock<IAdministratorLogic>();
            mockAdministratorLogic.Setup(m => m.FindAdministratorByCredentials(
                It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Administrator());

            var controller = new SessionController(mockSessionLogic.Object, mockAdministratorLogic.Object);
            var response = controller.Login(model);

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }
    }
}
