using ImmRequest.DataAccess.Context;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.DataAccess.Repositories;
using ImmRequest.Domain.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace ImmRequest.BusinessLogic.Tests
{
    [TestClass]
    public class AdministratorValidatorTest
    {
        Administrator administrator;

        [TestInitialize]
        public void SetUp()
        {
            administrator = new Administrator
            {
                UserName = "왕 잭슨",
                PassWord = "Aite Aite",
                Email = "jackson@wang.com"
            };
        }

        [TestMethod]
        public void IsValidAdminWithUniqueEmail()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Exists(It.IsAny<Administrator>()))
                .Returns(false);
            var validator = new AdministratorValidator(mockRepository.Object);
            var isValid = validator.IsValid(administrator);

            Assert.IsTrue(isValid);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void IsNotValidAdminWithRepeatedEmail()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Exists(It.IsAny<Administrator>()))
                .Returns(true);
            var validator = new AdministratorValidator(mockRepository.Object);
            var isValid = validator.IsValid(administrator);

            Assert.IsFalse(isValid);
            mockRepository.VerifyAll();
        }

        [TestMethod]
        public void IsValidAdminWithUniqueEmailUsingContext()
        {
            var context = ContextFactory.GetMemoryContext("Unique Email");
            var repository = new AdministratorRepository(context);
            repository.Insert(administrator);
            var validator = new AdministratorValidator(repository);
            var newAdmin = new Administrator
            {
                Email = "youngjae@email.com",
                UserName = "최 영재",
                PassWord = "1234"
            };

            var isValid = validator.IsValid(newAdmin);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsNotValidWithRepeatedEmailUsingContext()
        {
            var context = ContextFactory.GetMemoryContext("Repeated Email");
            var repository = new AdministratorRepository(context);
            repository.Insert(administrator);
            var validator = new AdministratorValidator(repository);
            var newAdmin = new Administrator
            {
                Email = "jackson@wang.com",
                UserName = "최 영재",
                PassWord = "1234"
            };

            var isValid = validator.IsValid(newAdmin);
            Assert.IsFalse(isValid);
        }

    }
}
