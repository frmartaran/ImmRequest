using System;
using System.Linq;
using ImmRequest.DataAccess.Context;
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
                RequestNumber = 1,
                Status = RequestStatus.Created,
                Area = new Area(),
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
    }
}