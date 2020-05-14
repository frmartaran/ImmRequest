using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain.UserManagement;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        private Session session;
        private ImmDbContext context;

        [TestInitialize]
        public void SetUp()
        {
            session = new Session
            {
                AdministratorInSession = new Administrator
                {
                    Email = "notbythemoon@song.com",
                    UserName = "Not by the moon",
                    Password = "1324"
                },
            };
        }

        [TestCleanup]
        public void TearDown()
        {
            if (context != null)
                context.Dispose();
        }

        private SessionLogic GetLogicWithMemoryDb(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            var repository = new SessionRepository(context);
            var validator = new SessionValidator(repository);
            var logic = new SessionLogic(repository, validator);
            return logic;
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
            var logic = GetLogicWithMemoryDb("ValidSession");
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
        public void GetSessionMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.GetAll())
                .Returns(new List<Session> { session });
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            var sessionInDb = logic.Get(session.Token);
            Assert.IsNotNull(sessionInDb);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetSessionTest()
        {
            var logic = GetLogicWithMemoryDb("Get Session");
            context.Sessions.Add(session);
            context.SaveChanges();

            var sessionInDb = logic.Get(session.Token);
            Assert.IsNotNull(sessionInDb);

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.GetAll())
                .Returns(new List<Session>());
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            var sessionInDb = logic.Get(session.Token);
            Assert.IsNotNull(sessionInDb);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundTest()
        {
            var logic = GetLogicWithMemoryDb("Get not founc Session");
            logic.Get(session.Token);

        }


        [TestMethod]
        public void DeleteMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()));
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            logic.Delete(1);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void DeleteTest()
        {
            var logic = GetLogicWithMemoryDb("Delete Test");
            context.Sessions.Add(session);
            context.SaveChanges();

            logic.Delete(1);

            var sessionInDb = context.Sessions.FirstOrDefault();
            Assert.IsNull(sessionInDb);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void DeleteIfNotFoundTest()
        {
            var logic = GetLogicWithMemoryDb("Delete not found Test");
            logic.Delete(1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void DeleteIfNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()))
                .Throws(new DatabaseNotFoundException(""));
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            logic.Delete(1);
            mockRepository.VerifyAll();
        }


        [TestMethod]
        public void IsValidTokenMockTest()
        {
            var mockRespository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRespository.Setup(m => m.GetAll())
                .Returns(new List<Session> { session });
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRespository.Object, mockValidator);
            logic.IsValidToken(session.Token);

            mockRespository.VerifyAll();
        }

        [TestMethod]
        public void IsValidTokenTest()
        {
            var logic = GetLogicWithMemoryDb("Valid Token");
            context.Sessions.Add(session);
            context.SaveChanges();

            var isValid = logic.IsValidToken(session.Token);
            Assert.IsTrue(isValid);

        }

        [TestMethod]
        public void IsInvalidTokenMockTest()
        {
            var mockRespository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRespository.Setup(m => m.GetAll())
                .Returns(new List<Session>());
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRespository.Object, mockValidator);
            logic.IsValidToken(session.Token);

            mockRespository.VerifyAll();
        }

        [TestMethod]
        public void IsInvalidTokenTest()
        {
            var logic = GetLogicWithMemoryDb("Invalid Token");

            var isValid = logic.IsValidToken(session.Token);
            Assert.IsFalse(isValid);

        }

    }
}
