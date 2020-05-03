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
            var repository = CreateRepositoryWithContext("Find Test");

            var finder = new TopicFinder(repository);

            var topicFound = finder.Find(t => t.Id == 1);
            Assert.AreEqual(topic.Name, topicFound.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void FindAreaWithConditionMockTest()
        {
            var mock = new Mock<IRepository<Topic>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll())
                .Returns(new List<Topic> { topic });

            var finder = new TopicFinder(mock.Object);
            var areaFound = finder.Find(t => t.Id == 1);
            mock.VerifyAll();
        }
    }
}
