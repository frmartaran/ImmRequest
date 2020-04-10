using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repostories;
using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class SessionValidatorTest
    {
        private Session session;

        [TestInitialize]
        public void Setup()
        {
            session = new Session
            {
                AdministratorInSession = new Administrator(),
                Token = Guid.NewGuid()
            };

        }

        [TestMethod]
        public void IsValidMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>();
            var validator = new SessionValidator(mockRepository.Object);
            var isValid = validator.IsValid(session);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidTest()
        {
            var context = ContextFactory.GetMemoryContext("Session is Valid");
            var repository = new SessionRepository(context);
            var validator = new SessionValidator(repository);

            var isValid = validator.IsValid(session);
            Assert.IsTrue(isValid);
        }
    }
}
