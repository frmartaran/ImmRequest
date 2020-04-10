﻿using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic;
using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
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
        public void CreateValidAdminMock()
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
        public void CreateValidAdministrator()
        {
            var context = ContextFactory.GetMemoryContext("Valid Admin");
            var repository = new AdministratorRepository(context);
            var validator = new AdministratorValidator(repository);

            var logic = new AdministratorLogic(repository, validator);
            logic.Create(administrator);

            var adminInDb = context.Administrators.FirstOrDefault();
            Assert.IsNotNull(adminInDb);

        }
    }
}
