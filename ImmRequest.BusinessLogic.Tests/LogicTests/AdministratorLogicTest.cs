using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Interfaces.Exceptions;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.LogicTests
{
    [TestClass]
    public class AdministratorLogicTest
    {
        Administrator administrator;

        [TestInitialize]
        public void SetUp()
        {
            administrator = new Administrator
            {
                UserName = "김 유겸",
                PassWord = "654",
                Email = "hitthestage@yugyeom.com"
            };
        }

        [TestMethod]
        public void CreateValidAdminMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<Administrator>()));
            var mockValidator = new Mock<IValidator<Administrator>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Administrator>()))
                .Returns(true);
            var logic = new AdministratorLogic(mockRepository.Object, mockValidator.Object);
            logic.Create(administrator);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void CreateValidAdministratorTest()
        {
            var context = ContextFactory.GetMemoryContext("Valid Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);

            var logic = new AdministratorLogic(repository, validator);
            logic.Create(administrator);

            var adminInDb = context.Administrators.FirstOrDefault();
            Assert.IsNotNull(adminInDb);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void CreateInvalidAdministratorMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Insert(It.IsAny<Administrator>()));
            var mockValidator = new Mock<IValidator<Administrator>>(MockBehavior.Strict);
            mockValidator.Setup(m => m.IsValid(It.IsAny<Administrator>()))
                .Throws(new ValidationException(""));
            var logic = new AdministratorLogic(mockRepository.Object, mockValidator.Object);
            administrator.UserName = "";
            logic.Create(administrator);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void CreateInvalidAdministratorTest()
        {
            var context = ContextFactory.GetMemoryContext("Invalid Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);

            var logic = new AdministratorLogic(repository, validator);
            administrator.UserName = "";
            logic.Create(administrator);

            var adminInDb = context.Administrators.FirstOrDefault();

        }

        [TestMethod]
        public void DeleteAdministratorMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()));
            var mockValidator = new Mock<IValidator<Administrator>>().Object;
            var logic = new AdministratorLogic(mockRepository.Object, mockValidator);
            logic.Delete(administrator.Id);
            mockRepository.VerifyAll();

        }

        [TestMethod]
        public void DeleteAdministratorTest()
        {
            var context = ContextFactory.GetMemoryContext("Delete Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);
            repository.Insert(administrator);

            var logic = new AdministratorLogic(repository, validator);
            logic.Delete(administrator.Id);

            var adminInDb = context.Administrators.FirstOrDefault();
            Assert.IsNull(adminInDb);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]
        public void DeleteAdministratorNotFoundTest()
        {
            var context = ContextFactory.GetMemoryContext("Delete Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);

            var logic = new AdministratorLogic(repository, validator);
            logic.Delete(administrator.Id);

        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void DeleteAdministratorNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Delete(It.IsAny<long>()))
                .Throws(new DatabaseNotFoundException(""));
            var mockValidator = new Mock<IValidator<Administrator>>().Object;

            var logic = new AdministratorLogic(mockRepository.Object, mockValidator);
            logic.Delete(administrator.Id);

            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetAdministratorMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(administrator);
            var mockValidator = new Mock<IValidator<Administrator>>().Object;

            var logic = new AdministratorLogic(mockRepository.Object, mockValidator);
            var admin = logic.Get(1);

            Assert.IsNotNull(admin);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetAdministratorTest()
        {
            var context = ContextFactory.GetMemoryContext("Get Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);
            repository.Insert(administrator);
            var logic = new AdministratorLogic(repository, validator);
            var admin = logic.Get(administrator.Id);

            Assert.IsNotNull(admin);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void GetAdministratorNotFound()
        {
            var context = ContextFactory.GetMemoryContext("Get Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);
            var logic = new AdministratorLogic(repository, validator);
            var admin = logic.Get(administrator.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException))]

        public void GetAdministratorNotFoundMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Get(It.IsAny<long>()))
                .Returns<Administrator>(null);
            var mockValidator = new Mock<IValidator<Administrator>>().Object;

            var logic = new AdministratorLogic(mockRepository.Object, mockValidator);
            var admin = logic.Get(1);

            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void GetAllMockTest()
        {
            var allAdministrator = new List<Administrator> { administrator };
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.GetAll())
                .Returns(allAdministrator);
            var mockValidator = new Mock<IValidator<Administrator>>().Object;

            var logic = new AdministratorLogic(mockRepository.Object, mockValidator);
            var administrators = logic.GetAll();

            mockRepository.VerifyAll();
            Assert.AreEqual(1, administrators.Count);
        }

        [TestMethod]
        public void GetAllTest()
        {
            var context = ContextFactory.GetMemoryContext("Get All Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);
            repository.Insert(administrator);
            var logic = new AdministratorLogic(repository, validator);
            var administrators = logic.GetAll();

            Assert.AreEqual(1, administrators.Count);
        }

        [TestMethod]
        public void UpdateMockTest()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Update(It.IsAny<Administrator>()));
            var mockValidator = new Mock<IValidator<Administrator>>();
            mockValidator.Setup(m => m.IsValid(It.IsAny<Administrator>()))
                .Returns(true);

            administrator.UserName = "Julie";
            var logic = new AdministratorLogic(mockRepository.Object, mockValidator.Object);
            logic.Update(administrator);

            mockRepository.VerifyAll();
            mockValidator.VerifyAll();
        }

        [TestMethod]
        public void UpdateTest()
        {
            var context = ContextFactory.GetMemoryContext("Update Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);
            repository.Insert(administrator);
            administrator.PassWord = "852";
            var logic = new AdministratorLogic(repository, validator);
            logic.Update(administrator);

            var adminInDb = context.Administrators.FirstOrDefault();
            Assert.AreEqual("852", adminInDb.PassWord);

        }

    }
}
