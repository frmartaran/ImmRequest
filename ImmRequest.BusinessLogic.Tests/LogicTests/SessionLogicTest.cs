using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.DataAccess.Repostories;
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
                    PassWord = "1324"
                },
                Token = Guid.NewGuid()
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
        [ExpectedException(typeof(ValidationException))]

        public void CreateInvalidSessionTest()
        {
            var logic = GetLogicWithMemoryDb("Invalid Session");
            session.Token = Guid.Empty;
            logic.Create(session);
        }

        [TestMethod]
        public void GetSessionMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(session);
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            var sessionInDb = logic.Get(1);
            Assert.IsNotNull(sessionInDb);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetSessionTest()
        {
            var logic = GetLogicWithMemoryDb("Get Session");
            context.Sessions.Add(session);
            context.SaveChanges();

            var sessionInDb = logic.Get(1);
            Assert.IsNotNull(sessionInDb);

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<Session>(null);
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            var sessionInDb = logic.Get(1);
            Assert.IsNotNull(sessionInDb);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundTest()
        {
            var logic = GetLogicWithMemoryDb("Get not founc Session");
            logic.Get(1);

        }

        [TestMethod]
        public void GetAllTest()
        {
            var logic = GetLogicWithMemoryDb("Get All Test");
            context.Sessions.Add(session);
            context.SaveChanges();

            var allSessions = logic.GetAll();

            Assert.AreEqual(1, allSessions.Count);
        }

        [TestMethod]
        public void GetAllMockTest()
        {
            var sessions = new List<Session> { session };
            var mockRepository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.GetAll())
                .Returns(sessions);
            var mockValidator = new Mock<IValidator<Session>>().Object;
            var logic = new SessionLogic(mockRepository.Object, mockValidator);
            var allSessions = logic.GetAll();
            Assert.AreEqual(1, allSessions.Count);
            mockRepository.VerifyAll();
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
        public void UpdateTest()
        {
            var logic = GetLogicWithMemoryDb("Update Test");
            context.Sessions.Add(session);
            context.SaveChanges();

            var newToken = Guid.NewGuid();
            session.Token = newToken;

            logic.Update(session);
            var sessionInDb = context.Sessions.FirstOrDefault();
            Assert.AreEqual(newToken, sessionInDb.Token);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InvalidUpdateTest()
        {
            var logic = GetLogicWithMemoryDb("Update Not Valid Test");
            context.Sessions.Add(session);
            context.SaveChanges();

            var newToken = Guid.Empty;
            session.Token = newToken;

            logic.Update(session);
        }

        [TestMethod]
        public void UpdateMockTest()
        {
            var mockRespository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRespository.Setup(m => m.Update(It.IsAny<Session>()))
                .Returns(session);
            var mockValidator = new Mock<IValidator<Session>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Session>()))
                .Returns(true);

            var logic = new SessionLogic(mockRespository.Object, mockValidator.Object);
            logic.Update(session);

            mockRespository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void InvalidUpdateMockTest()
        {
            var mockRespository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRespository.Setup(m => m.Update(It.IsAny<Session>()))
                .Returns(session);
            var mockValidator = new Mock<IValidator<Session>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Session>()))
                .Throws(new ValidationException(""));

            var logic = new SessionLogic(mockRespository.Object, mockValidator.Object);
            logic.Update(session);

            mockRespository.VerifyAll();
            mockValidator.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void UpdateNotFoundTest()
        {
            var logic = GetLogicWithMemoryDb("Update Not Found Test");
            logic.Update(session);

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void UpdateNotFoundMockTest()
        {
            var mockRespository = new Mock<IRepository<Session>>(MockBehavior.Strict);
            mockRespository.Setup(m => m.Update(It.IsAny<Session>()))
                .Throws(new DatabaseNotFoundException(""));
            var mockValidator = new Mock<IValidator<Session>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Session>()))
                .Returns(true);

            var logic = new SessionLogic(mockRespository.Object, mockValidator.Object);
            logic.Update(session);

            mockRespository.VerifyAll();
            mockValidator.VerifyAll();
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
