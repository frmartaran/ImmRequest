using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.LogicTests
{
    [TestClass]
    public class TopicTypeLogicTest
    {
        private Area area;
        private Topic topic;
        private TopicType type;
        private NumberField field;
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
                Name = "Topic",
                Area = area,
                Types = new List<TopicType>(),
            };

            type = new TopicType
            {
                Name = "Type",
                ParentTopic = topic,
                AllFields = new List<BaseField>()
            };

            field = new NumberField
            {
                RangeStart = 1,
                RangeEnd = 10,
                Name = "Name"
            };
            type.AllFields.Add(field);

        }

        private TopicTypeLogic CreateLogicWithRepositoryAndValidator(string name)
        {
            context = ContextFactory.GetMemoryContext(name);
            context.Areas.Add(area);
            context.Topics.Add(topic);
            context.SaveChanges();

            var repository = new TopicTypeRepository(context);
            var validator = new TopicTypeValidator();
            return new TopicTypeLogic(repository, validator);
        }

        [TestMethod]
        public void CreateMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<TopicType>()));

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<TopicType>()))
               .Returns(true);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(type);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void CreateTest()
        {

            var logic = CreateLogicWithRepositoryAndValidator("Create Test");
            logic.Create(type);
            logic.Save();

            var typeInDb = context.TopicTypes.FirstOrDefault();
            var fieldInDb = context.Fields.FirstOrDefault();

            Assert.IsNotNull(typeInDb);
            Assert.IsNotNull(fieldInDb);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvalidMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<TopicType>()));

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<TopicType>()))
               .Throws(new ValidationException(""));

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(type);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();

            var typeInDb = context.TopicTypes.FirstOrDefault();
            var fieldInDb = context.Fields.FirstOrDefault();

            Assert.IsNull(typeInDb);
            Assert.IsNull(fieldInDb);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void CreateInvalidTest()
        {
            field.RangeEnd = -5;
            var logic = CreateLogicWithRepositoryAndValidator("Create Invalid Test");
            logic.Create(type);
        }

        [TestMethod]
        public void GetTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Get Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();
            var typeInDb = logic.Get(1);

            Assert.IsNotNull(typeInDb);
            var fieldsCount = typeInDb.AllFields.Count();
            Assert.AreEqual(1, fieldsCount);

        }

        [TestMethod]
        public void GetMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(type);

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            var typeInDb = logic.Get(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void GetNotFoundTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Get Not Found Test");
            var typeInDb = logic.Get(1);

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void GetNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<TopicType>(null);

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            logic.Get(1);
            mockRepository.VerifyAll();
            mockValidator.VerifyAll();

        }

        [TestMethod]
        public void GetAllMockTest()
        {

            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.GetAll())
                .Returns(new List<TopicType> { type });

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            logic.GetAll();

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void GetAllTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Get All Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var all = logic.GetAll();
            Assert.AreEqual(1, all.Count);

        }

        [TestMethod]
        public void DeleteTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Delete Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            logic.Delete(1);
            logic.Save();

            var typeInDb = context.TopicTypes.FirstOrDefault();
            Assert.IsNull(typeInDb);
        }

        [TestMethod]
        public void DeleteMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()));

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            logic.Delete(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void DeleteNotFoundTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Delete Test");
            logic.Delete(1);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void DeleteNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()))
                .Throws(new DatabaseNotFoundException(""));

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            logic.Delete(1);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void UpdateTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Update Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var newName = "Another Name";
            type.Name = newName;
            logic.Update(type);

            var typeInDb = context.TopicTypes.FirstOrDefault();
            Assert.AreEqual(newName, typeInDb.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateInvalidTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Update Invalid Test");
            context.TopicTypes.Add(type);
            context.SaveChanges();

            var newName = "";
            type.Name = newName;
            logic.Update(type);
        }

        [TestMethod]
        public void UpdateMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Update(It.IsAny<TopicType>()))
                .Returns(type);

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<TopicType>()))
                .Returns(true);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            var newName = "Another Name";
            type.Name = newName;
            logic.Update(type);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void UpdateInvalidMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Update(It.IsAny<TopicType>()));

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<TopicType>()))
                .Throws(new ValidationException(""));

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            var newName = "";
            type.Name = newName;
            logic.Update(type);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }


        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void UpdateNotFoundTest()
        {
            var logic = CreateLogicWithRepositoryAndValidator("Update Not Found Test");

            var newName = "Some Name";
            type.Name = newName;
            logic.Update(type);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void UpdateNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<TopicType>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Update(It.IsAny<TopicType>()))
                .Throws(new DatabaseNotFoundException(""));

            var mockValidator = new Mock<IValidator<TopicType>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<TopicType>()))
                .Returns(true);

            var logic = new TopicTypeLogic(mockRepository.Object, mockValidator.Object);
            var newName = "";
            type.Name = newName;
            logic.Update(type);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }




    }
}
