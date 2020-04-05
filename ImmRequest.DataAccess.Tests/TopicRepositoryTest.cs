using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImmRequest.DataAccess.Tests
{
    [TestClass]
    public class TopicRepositoryTest
    {
        ImmDbContext context;
        Topic topic;
        TopicRepository repository;

        [TestInitialize]
        public void Setup()
        {
            topic = new Topic
            {
                Name = "Topic Test",
                Types = new List<TopicType>()
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
            repository = new TopicRepository(context);
        }

        [TestMethod]
        public void SaveTest()
        {
            CreateRepository("Save Test");
            context.Topics.Add(topic);
            repository.Save();

            var topicInDb = context.Topics.FirstOrDefault();
            Assert.IsNotNull(topicInDb);
        }
    }
}