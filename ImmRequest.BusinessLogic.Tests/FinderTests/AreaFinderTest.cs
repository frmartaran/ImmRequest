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
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.FinderTests
{
    [TestClass]
    public class AreaFinderTest
    {
        private Area area;
        ImmDbContext context;

        [TestInitialize]
        public void Setup()
        {
            area = new Area
            {
                Name = "Some Area"
            };

        }

        private AreaRepository CreateRepositoryWithContext(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            return new AreaRepository(context);
        }

        [TestMethod]
        public void FindAreaTest()
        {
            var repository = CreateRepositoryWithContext("Find Test");
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new AreaFinder(repository);
            var areaFound = finder.Find(1);
            Assert.AreEqual(area.Name, areaFound.Name);
        }

        [TestMethod]
        public void FindAreaMockTest()
        {
            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(area);

            var finder = new AreaFinder(mock.Object);
            var areaFound = finder.Find(1);
            mock.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void NotFoundTest()
        {
            var repository = CreateRepositoryWithContext("Not Found Test");

            var finder = new AreaFinder(repository);
            var areaFound = finder.Find(1);
            Assert.IsNull(areaFound);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void NotFoundMockTest()
        {
            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<Area>(null);

            var finder = new AreaFinder(mock.Object);
            var areaFound = finder.Find(1);
            mock.VerifyAll();
        }

        [TestMethod]
        public void FindAreaWithConditionTest()
        {
            var repository = CreateRepositoryWithContext("Find Test");
            context.Areas.Add(area);
            context.SaveChanges();

            var finder = new AreaFinder(repository);

            var areaFound = finder.Find(a => a.Name == "Some Area");
            Assert.AreEqual(area.Name, areaFound.Name);
        }

        [TestMethod]
        public void FindAreaWithConditionMockTest()
        {
            var mock = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mock.Setup(m => m.GetAll())
                .Returns(new List<Area> { area });

            var finder = new AreaFinder(mock.Object);
            var areaFound = finder.Find(a => a.Name == "Some Area");
            mock.VerifyAll();
        }
    }
}
