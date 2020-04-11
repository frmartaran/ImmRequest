﻿using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repostories;
using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.LogicTests
{
    [TestClass]
    public class SessionLogicTest
    {
        Session session;

        [TestInitialize]
        public void SetUp()
        {
            session = new Session
            {
                AdministratorInSession = new Administrator(),
                Token = Guid.NewGuid()
            };
        }

        [TestMethod]
        public void CreateValidSessionMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<Session>()));
            var mockValidator = new Mock<IValidator<Session>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Session>()))
                .Returns(true);

            var logic = new SessionLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(session);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void CreateValidSessionTest()
        {
            var context = ContextFactory.GetMemoryContext("Valid Session");
            var repository = new SessionRepository(context);
            var validator = new SessionValidator(repository);
            var logic = new SessionLogic(repository, validator);
            logic.Create(session);

            var sessionCreated = context.Sessions.FirstOrDefault();
            Assert.IsNotNull(sessionCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvalidSessionMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<Session>()));
            var mockValidator = new Mock<IValidator<Session>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Session>()))
                .Throws(new ValidationException(""));

            var logic = new SessionLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(session);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void CreateInvalidSessionTest()
        {
            var context = ContextFactory.GetMemoryContext("Valid Session");
            var repository = new SessionRepository(context);
            var validator = new SessionValidator(repository);
            var logic = new SessionLogic(repository, validator);
            session.Token = Guid.Empty;
            logic.Create(session);
        }
    }
}
