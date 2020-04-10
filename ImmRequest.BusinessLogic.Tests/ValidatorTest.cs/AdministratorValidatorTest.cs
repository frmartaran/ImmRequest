using ImmRequest.DataAccess.Interfaces;
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
        public void IsValidAdmin()
        {
            var mockRepository = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Exists(It.IsAny<Administrator>()))
                .Returns(false);
            var validator = new AdministratorValidator(mockRepository.Object);
            var isValid = validator.IsValid(administrator);

            Assert.IsTrue(isValid);
            mockRepository.VerifyAll();
        }
    }
}
