using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class TopicTypeRepositoryTest
    {
        private ImmDbContext context;

        private TopicType type;

        private TopicTypeRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            type = new TopicType
            {
                Name = "Some Type",
                AllFields = new List<BaseField>()
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
            repository = new TopicTypeRepository(context);
        }

        [TestMethod]
        public void SaveTest()
        {
            CreateRepository("Type Save Test");
            context.TopicTypes.Add(type);
            repository.Save();

            var typeInDb = context.TopicTypes.FirstOrDefault();
            Assert.IsNotNull(typeInDb);
        }


    }
}