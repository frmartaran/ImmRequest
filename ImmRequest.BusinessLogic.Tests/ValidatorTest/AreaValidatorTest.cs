using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class AreaValidatorTest
    {

        private Area area;
        [TestInitialize]
        public void Setup()
        {
            area = new Area
            {
                Name = "Area",
                Topics = new List<Topic> { new Topic() }
            };
        }

        [TestMethod]
        public void AreaIsValid()
        {
            var mockRepository = new Mock<IRepository<Area>>();
            var validator = new AreaValidator(mockRepository.Object);
            var isValid = validator.IsValid(area);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void AreaWithoutNameIsInvalid()
        {
            var mockRepository = new Mock<IRepository<Area>>();
            var validator = new AreaValidator(mockRepository.Object);
            area.Name = "";
            validator.IsValid(area);

        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void AreaWithRepeatedNameIsInvalid()
        {
            var mockRepository = new Mock<IRepository<Area>>(MockBehavior.Strict);
            mockRepository.Setup(m => m.Exists(It.IsAny<Area>()))
                .Returns(true);
            var validator = new AreaValidator(mockRepository.Object);
            validator.IsValid(area);
            mockRepository.VerifyAll();

        }

    }
}
