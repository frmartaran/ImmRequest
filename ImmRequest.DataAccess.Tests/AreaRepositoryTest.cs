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
    public class AreaRepositoryTest
    {
        Area area;
        ImmDbContext context;
        AreaRepository repository;

        [TestInitialize]
        public void SetUp()
        {
            area = new Area
            {
                Name = "Test Area",
                Topics = new List<Topic>()
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
            repository = new AreaRepository(context);
        }

        [TestMethod]
        public void InsertTest()
        {
            CreateRepository("InsertTest");
            repository.Insert(area);

            var areaInDb = context.Areas.FirstOrDefault();
            Assert.IsNotNull(areaInDb);
        }

        [TestMethod]
        public void SaveTest()
        {
            CreateRepository("SaveTest");
            context.Areas.Add(area);
            repository.Save();

            var areaInDb = context.Areas.FirstOrDefault();
            Assert.IsNotNull(areaInDb);
        }

        [TestMethod]
        public void GetTest()
        {
            CreateRepository("GetTest");
            context.Areas.Add(area);
            context.SaveChanges();

            var areaInDb = repository.Get(1);
            Assert.IsNotNull(areaInDb);
        }

        [TestMethod]
        public void GetAllTest()
        {
            CreateRepository("GetAllTest");
            context.Areas.Add(area);
            context.SaveChanges();

            var area2 = new Area
            {
                Name = "Area 2 Test",
                Topics = new List<Topic>()
            };

            context.Areas.Add(area2);
            context.SaveChanges();

            var allAreas = repository.GetAll();
            Assert.AreEqual(2, allAreas.Count);
        }

        [TestMethod]
        public void DeleteTest()
        {
            CreateRepository("DeleteTest");
            context.Areas.Add(area);
            context.SaveChanges();

            repository.Delete(1);
            var areaInDb = context.Areas.FirstOrDefault();
            Assert.IsNull(areaInDb);

        }
    }
}