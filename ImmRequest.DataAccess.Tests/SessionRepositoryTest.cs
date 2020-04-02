using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Exceptions;
using ImmRequest.DataAccess.Repostories;
using ImmRequest.Domain.UserManagement;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class SessionRepositoryTest
    {
        private Session session;
        private Administrator administrator;
        private ImmDbContext context;
        private SessionRepository repository;

        [TestInitialize]
        public void SetUp()
        {

            administrator = new Administrator
            {
                UserName = "임 재범",
                PassWord = "1234"
            };

            session = new Session
            {
                AdministratorInSession = administrator,
                Token = Guid.NewGuid()
            };
        }

        [TestCleanup]
        public void TearDown()
        {
            context.Dispose();
        }

        private void CreateRepository(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            repository = new SessionRepository(context);
        }

        [TestMethod]
        public void InsertTest()
        {
            CreateRepository("InsertTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            session.AdministratorId = administrator.Id;
            repository.Insert(session);

            var sessionCount = context.Sessions.Count();
            Assert.AreEqual(1, sessionCount);
        }

        [TestMethod]
        public void GetTest()
        {
            CreateRepository("GetTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            session.AdministratorId = administrator.Id;
            context.Sessions.Add(session);
            context.SaveChanges();

            var sessionInDb = repository.Get(1);
            Assert.IsNotNull(sessionInDb);

        }

        [TestMethod]
        public void DeleteTest()
        {
            CreateRepository("DeleteTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            session.AdministratorId = administrator.Id;
            context.Sessions.Add(session);
            context.SaveChanges();

            repository.Delete(session.Id);
            var sessionCount = context.Sessions.Count();
            Assert.AreEqual(0, sessionCount);

        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void DeleteNotFound()
        {
            CreateRepository("DeleteNotFound");
            repository.Delete(1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            CreateRepository("UpdateTest");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            session.AdministratorId = administrator.Id;
            context.Sessions.Add(session);
            context.SaveChanges();

            var newToken = Guid.NewGuid();
            session.Token = newToken;

            repository.Update(session);
            var sessionInDb = context.Sessions.FirstOrDefault();
            Assert.AreEqual(newToken, sessionInDb.Token);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void UpdateNotFound()
        {
            CreateRepository("UpdateNotFoundTest");
            repository.Update(session);
        }

        [TestMethod]
        public void GetAll()
        {
            CreateRepository("GetAll");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            session.AdministratorId = administrator.Id;
            context.Sessions.Add(session);
            context.SaveChanges();

            var session2 = new Session
            {
                AdministratorInSession = administrator,
                Token = Guid.NewGuid()
            };
            context.Sessions.Add(session2);
            context.SaveChanges();

            var allSessions = repository.GetAll();
            Assert.AreEqual(2, allSessions.Count);

        }

        [TestMethod]
        public void Exists()
        {
            CreateRepository("Exists");
            context.Administrators.Add(administrator);
            context.SaveChanges();

            session.AdministratorId = administrator.Id;
            context.Sessions.Add(session);
            context.SaveChanges();

            var exists = repository.Exists(session.Id);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void DoesNotExists()
        {
            CreateRepository("DoesNotExists");

            var exists = repository.Exists(session.Id);
            Assert.IsFalse(exists);
        }
    }
}
