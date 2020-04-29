using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class CitizenRequestRepositoryTest
    {

        private ImmDbContext context;
        private CitizenRequestRepository repository;
        private CitizenRequest request;

        [TestInitialize]
        public void SetUp()
        {
            request = new CitizenRequest
            {
                Description = "A Request",
                CitizenName = "박 진영",
                Email = "example@gmail.com",
                Phone = "099123456",
                Status = RequestStatus.Created,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType(),
                Values = new List<RequestFieldValues>()
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
            repository = new CitizenRequestRepository(context);
        }

        [TestMethod]
        public void InsertTest()
        {
            CreateRepository("InsertTest");
            repository.Insert(request);

            var requestInDb = context.CitizenRequests.FirstOrDefault();
            Assert.IsNotNull(requestInDb);
        }

        [TestMethod]
        public void GetTest()
        {
            CreateRepository("GetTest");
            context.CitizenRequests.Add(request);
            context.SaveChanges();

            var requestInDb = repository.Get(1);
            Assert.IsNotNull(requestInDb);
        }

        [TestMethod]
        public void DeleteTest()
        {
            CreateRepository("DeleteTest");
            context.CitizenRequests.Add(request);
            context.SaveChanges();

            repository.Delete(1);
            var requestInDb = context.CitizenRequests.FirstOrDefault();
            Assert.IsNull(requestInDb);
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
            context.CitizenRequests.Add(request);
            context.SaveChanges();

            request.CitizenName = "Julie";
            repository.Update(request);

            var requestInDb = context.CitizenRequests.FirstOrDefault();
            Assert.AreEqual("Julie", requestInDb.CitizenName);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void UpdateNotFound()
        {
            CreateRepository("UpdateNotFound");
            repository.Update(request);
        }

        [TestMethod]
        public void GetAllTest()
        {
            CreateRepository("GetAllTest");
            context.CitizenRequests.Add(request);
            context.SaveChanges();

            var request2 = new CitizenRequest
            {
                Description = "A Request",
                CitizenName = "최 영재",
                Email = "example2@gmail.com",
                Phone = "099123453",
                Status = RequestStatus.OnRevision,
                Area = new Area(),
                Topic = new Topic(),
                TopicType = new TopicType(),
                Values = new List<RequestFieldValues>()
            };

            context.CitizenRequests.Add(request2);
            context.SaveChanges();

            var allRequest = repository.GetAll();
            Assert.AreEqual(2, allRequest.Count);
        }

        [TestMethod]
        public void ExistsTest()
        {
            CreateRepository("Exists");
            context.CitizenRequests.Add(request);
            context.SaveChanges();

            var exists = repository.Exists(request);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void DoesNotExistsTest()
        {
            CreateRepository("DoesNotExists");

            var exists = repository.Exists(request);
            Assert.IsFalse(exists);
        }
    }
}