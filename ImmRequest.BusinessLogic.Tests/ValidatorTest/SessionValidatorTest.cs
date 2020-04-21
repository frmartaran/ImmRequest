using ImmRequest.BusinessLogic.Exceptions;
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
            mockRepository.Setup(r => r.Exists(It.IsAny<Session>()))
                .Returns(false);
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

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void IsNotValidWithtoutAnAdmin()
        {
            var mockRepository = new Mock<IRepository<Session>>();
            var validator = new SessionValidator(mockRepository.Object);
            session.AdministratorInSession = null;
            validator.IsValid(session);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void IsNotValidIfAnotherSessionAlreadyExistsMock()
        {
            var mockRepository = new Mock<IRepository<Session>>();
            mockRepository.Setup(r => r.Exists(It.IsAny<Session>()))
                .Returns(true);
            var validator = new SessionValidator(mockRepository.Object);
            validator.IsValid(session);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void IsNotValidIfAnotherSessionAlreadyExists()
        {
            var context = ContextFactory.GetMemoryContext("Session is not Valid");
            var repository = new SessionRepository(context);
            repository.Insert(session);
            var validator = new SessionValidator(repository);
            var newSession = new Session
            {
                AdministratorId = session.AdministratorId,
                AdministratorInSession = session.AdministratorInSession,
                Token = Guid.NewGuid()
            };
            validator.IsValid(newSession);
        }
    }
}
