using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repostories;
using ImmRequest.Domain.UserManagement;
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
    }
}
