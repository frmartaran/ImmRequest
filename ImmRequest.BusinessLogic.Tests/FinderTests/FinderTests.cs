using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Helpers;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.FinderTests
{
    [TestClass]
    public class FinderTests
    {
        private Area area;
        private Topic topic;
        private ImmDbContext context;

        [TestInitialize]
        public void Setup()
        {
            area = new Area
            {
                Name = "Some Area"
            };


            topic = new Topic
            {
                Area = area,
                Name = "Topic",
            };
        }

        [TestCleanup]
        public void TearDown()
        {
            if (context != null)
                context.Dispose();
        }

        private DatabaseFinder CreateDBFinderWithContext(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            context.SaveChanges();
            return new DatabaseFinder(context);
        }

        [TestMethod]
        public void FindTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find Test");
            context.Areas.Add(area);
            context.Topics.Add(topic);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);
            var topicFound = databaseFinder.Find<Topic>(t => t.Id == 1);
            Assert.AreEqual(topic.Name, topicFound.Name);
        }

        [TestMethod]
        public void FindMockTest()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.Find<Topic>(It.IsAny<Predicate<Topic>>()))
                .Returns(topic);

            var finder = new Finder(mock.Object);
            var areaFound = finder.Find<Topic>(t => t.Id == 1);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Not Found Topic Test");
            var finder = new Finder(databaseFinder);
            var topicFound = finder.Find<Topic>(t => t.Id == 1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundMockTest()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.Find<Topic>(It.IsAny<Predicate<Topic>>()))
                .Returns<Topic>(null);

            var finder = new Finder(mock.Object);
            var areaFound = finder.Find<Topic>(t => t.Id == 1);
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAllTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find All Test");
            context.Areas.Add(area);
            context.Topics.Add(topic);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);
            var topics = databaseFinder.FindAll<Topic>();
            Assert.AreEqual(1, topics.Count);
        }

        [TestMethod]
        public void FindAllMock()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.FindAll<Topic>())
                .Returns(new List<Topic> { topic });

            var finder = new Finder(mock.Object);
            var areaFound = finder.FindAll<Topic>();
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAllWithConditionTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find All With Condition Test");
            var otherTopic = new Topic
            {
                Area = area,
            };
            context.Topics.Add(topic);
            context.Topics.Add(otherTopic);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);

            var topics = finder.FindAll<Topic>(t => t.AreaId == area.Id);
            Assert.AreEqual(2, topics.Count);
        }

        [TestMethod]
        public void FindAllWithConditionMock()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.FindAll<Topic>(It.IsAny<Predicate<Topic>>()))
                .Returns(new List<Topic> { topic });

            var finder = new Finder(mock.Object);
            var areaFound = finder.FindAll<Topic>(t => t.AreaId == 1);
            mock.VerifyAll();
        }

        #region Area Tests

        [TestMethod]
        public void FindAreaWithConditionTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find Test");
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);

            var areaFound = finder.Find<Area>(a => a.Id == 1);
            Assert.AreEqual(area.Name, areaFound.Name);
        }

        [TestMethod]
        public void FindAreaWithConditionMockTest()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.Find<Area>(It.IsAny<Predicate<Area>>()))
                .Returns(area);

            var finder = new Finder(mock.Object);
            var areaFound = finder.Find<Area>(a => a.Id == 1);
            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundWithConditionTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find Test");
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);

            var areaFound = finder.Find<Area>(a => a.Name == "Area");
            Assert.AreEqual(area.Name, areaFound.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundWithConditionMockTest()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.Find<Area>(It.IsAny<Predicate<Area>>()))
                .Returns<Area>(null);

            var finder = new Finder(mock.Object);
            var areaFound = finder.Find<Area>(a => a.Name == "Area");
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAllAreaTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find Test");
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);

            var areas = finder.FindAll<Area>();
            Assert.AreEqual(1, areas.Count);
        }

        [TestMethod]
        public void FindAllAreaMock()
        {
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.FindAll<Area>())
                .Returns(new List<Area> { area });

            var finder = new Finder(mock.Object);
            var areaFound = finder.FindAll<Area>();
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAllAreaWithConditionTest()
        {
            var databaseFinder = CreateDBFinderWithContext("Find Test");
            var newArea = area;
            context.Areas.Add(newArea);
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new Finder(databaseFinder);

            var areas = finder.FindAll<Area>(a => a.Name == "Some Area");
            Assert.AreEqual(2, areas.Count);
        }

        [TestMethod]
        public void FindAllAreaWithConditionMock()
        {
            var newArea = area;
            var mock = new Mock<IDatabaseFinder>(MockBehavior.Strict);
            mock.Setup(m => m.FindAll<Area>(It.IsAny<Predicate<Area>>()))
                .Returns(new List<Area> { area, newArea });

            var finder = new Finder(mock.Object);
            var areaFound = finder.FindAll<Area>(a => a.Name == "Some Area");
            mock.VerifyAll();
        }
        #endregion
    }
}
