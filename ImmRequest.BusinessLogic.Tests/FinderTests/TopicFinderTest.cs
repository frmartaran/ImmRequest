using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.FinderTests
{
    [TestClass]
    public class TopicFinderTest
    {
        private Area area;
        private Topic topic;
        private ImmDbContext context;

        [TestInitialize]
        public void Setup()
        {
            area = new Area
            {
                Name = "Area"
            };


            topic = new Topic
            {
                Area = area,
                Name = "Topic",
            };
        }

        private TopicRepository CreateRepositoryWithContext(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            context.Areas.Add(area);
            context.SaveChanges();
            return new TopicRepository(context);
        }

        [TestMethod]
        public void FindTest()
        {
            var repository = CreateRepositoryWithContext("Find Test");
            context.Topics.Add(topic);
            context.SaveChanges();

            var finder = new TopicFinder(repository);

            var topicFound = finder.Find(t => t.Id == 1);
            Assert.AreEqual(topic.Name, topicFound.Name);
        }

        [TestMethod]
        public void FindMockTest()
        {
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll())
                .Returns(new List<Topic> { topic });
            topic.Id = 1;

            var finder = new TopicFinder(mock.Object);
            var areaFound = finder.Find(t => t.Id == 1);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundTest()
        {
            var repository = CreateRepositoryWithContext("Not Found Test");

            var finder = new TopicFinder(repository);

            var topicFound = finder.Find(t => t.Id == 1);
            Assert.AreEqual(topic.Name, topicFound.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundMockTest()
        {
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll())
                .Returns(new List<Topic> { topic });

            var finder = new TopicFinder(mock.Object);
            var areaFound = finder.Find(t => t.Id == 1);
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAllTest()
        {
            var repository = CreateRepositoryWithContext("Find All Test");
            context.Topics.Add(topic);
            context.SaveChanges();

            var finder = new TopicFinder(repository);

            var topics = finder.FindAll();
            Assert.AreEqual(1, topics.Count);
        }

        [TestMethod]
        public void FindAllMock()
        {
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll())
                .Returns(new List<Topic> { topic });

            var finder = new TopicFinder(mock.Object);
            var areaFound = finder.FindAll();
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAllWithConditionTest()
        {
            var repository = CreateRepositoryWithContext("Find All With Condition Test");
            var otherTopic = new Topic
            {
                Area = area,
            };
            context.Topics.Add(topic);
            context.Topics.Add(otherTopic);
            context.SaveChanges();

            var finder = new TopicFinder(repository);

            var topics = finder.FindAll(t => t.AreaId == area.Id);
            Assert.AreEqual(2, topics.Count);
        }

        [TestMethod]
        public void FindAllWithConditionMock()
        {
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            var otherTopic = new Topic
            {
                Area = area,
            };
            mock.Setup(m => m.GetAll())
                .Returns(new List<Topic> { topic, otherTopic });
            area.Id = 1;

            var finder = new TopicFinder(mock.Object);
            var areaFound = finder.FindAll(t => t.AreaId == 1);
            mock.VerifyAll();
        }
    }
}
