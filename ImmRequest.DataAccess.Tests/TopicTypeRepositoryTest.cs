using System;
using System.Collections.Generic;
using System.Linq;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces.Exceptions;
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
                AllFields = new List<BaseField>(),
                ParentTopic = new Topic()
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

        [TestMethod]
        public void InsertTest()
        {
            CreateRepository("Type Insert Test");
            repository.Insert(type);

            var typeInDb = context.TopicTypes.FirstOrDefault();
            Assert.IsNotNull(typeInDb);
        }

        [TestMethod]
        public void GetTest()
        {
            CreateRepository("Type Get Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var typeInDb = repository.Get(1);
            Assert.IsNotNull(typeInDb);
        }

        [TestMethod]
        public void GetAllTest()
        {
            CreateRepository("Type Get All Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var type2 = new TopicType
            {
                Name = "Some Type 2",
                AllFields = new List<BaseField>(),
                ParentTopic = new Topic()
            };
            context.TopicTypes.Add(type2);
            context.SaveChanges();

            var allTypes = repository.GetAll();
            Assert.AreEqual(2, allTypes.Count);

        }

        [TestMethod]
        public void ExistsTest()
        {
            CreateRepository("Type Exist Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var exists = repository.Exists(type);
            Assert.IsTrue(exists);
        }


        [TestMethod]
        public void DoesNotExistsTest()
        {
            CreateRepository("Type Does Not Exist Test");

            var exists = repository.Exists(type);
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public void DeleteTest()
        {
            CreateRepository("Type Delete Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            repository.Delete(1);
            var typeInDb = context.TopicTypes.FirstOrDefault();
            Assert.IsNull(typeInDb);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void DeleteNotFoundTest()
        {
            CreateRepository("Type Delete Not Found");
            repository.Delete(1);
        }

        [TestMethod]
        public void UpdateTest()
        {
            CreateRepository("Type Update Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            type.Name = "Another Name";
            repository.Update(type);

            var typeInDb = context.TopicTypes.FirstOrDefault();
            Assert.AreEqual("Another Name", typeInDb.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(DatabaseNotFoundException))]
        public void UpdateNotFoundTest()
        {
            CreateRepository("Type Update Not Found Test");
            repository.Update(type);
        }


    }
}