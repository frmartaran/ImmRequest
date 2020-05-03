using ImmRequest.BusinessLogic.Logic.Finders;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}
