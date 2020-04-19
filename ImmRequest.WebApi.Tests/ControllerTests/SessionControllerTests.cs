using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Models.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                AdministratorInSession = new AdministratorModel
                {
                    Email = "example@example.com",
                    Password = "1234",
                    Username = "Example"
                },
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
            Assert.AreEqual(model.AdministratorInSession.Email, newSession.AdministratorInSession.Email);
            Assert.AreEqual(model.Token, newSession.Token);
        }
    }
}
