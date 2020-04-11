﻿using ImmRequest.BusinessLogic.Exceptions;
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

            var adminInDb = context.Administrators.FirstOrDefault();
        }

        [TestMethod]
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

    }
}
