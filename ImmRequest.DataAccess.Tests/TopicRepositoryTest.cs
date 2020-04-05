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
                Types = new List<TopicType>(),
                Area = new Area()
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

        [TestMethod]
        public void InsertTest()
        {
            CreateRepository("InsertTest");
            repository.Insert(topic);

            var topicInDb = context.Topics.FirstOrDefault();
            Assert.IsNotNull(topicInDb);
        }

        [TestMethod]
        public void GetTest()
        {
            CreateRepository("Get Test");
            context.Topics.Add(topic);
            context.SaveChanges();

            var topicInDb = repository.Get(1);
            Assert.IsNotNull(topicInDb);
        }

        [TestMethod]
        public void GetAllTest()
        {
            CreateRepository("Topic Get All Test");
            context.Topics.Add(topic);
            context.SaveChanges();

            var topic2 = new Topic
            {
                Area = new Area(),
                Name = "Topic 2"
            };

            context.Topics.Add(topic2);
            context.SaveChanges();

            var allTopics = repository.GetAll();
            Assert.AreEqual(2, allTopics.Count);
        }

        [TestMethod]
        public void ExistsTest()
        {
            CreateRepository("Topic Exists Test");
            context.Topics.Add(topic);
            context.SaveChanges();

            var exists = repository.Exists(1);
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public void DoesNotExistsTest()
        {
            CreateRepository("Topic Does Not Exists Test");

            var exists = repository.Exists(1);
            Assert.IsFalse(exists);
        }


        [TestMethod]
        public void DeleteTest()
        {
            CreateRepository("Topic Delete Test");
            context.Topics.Add(topic);
            context.SaveChanges();

            repository.Delete(1);
            var topicInDb = context.Topics.FirstOrDefault();
            Assert.IsNull(topicInDb);
        }
    }
}