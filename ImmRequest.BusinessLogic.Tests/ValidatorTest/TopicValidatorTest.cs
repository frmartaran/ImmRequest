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
    public class TopicValidatorTest
    {

        private Topic topic;

        [TestInitialize]
        public void Setup()
        {
            topic = new Topic
            {
                Area = new Area(),
                Name = "Topic",
                Types = new List<TopicType>()
            };
        }

        [TestMethod]
        public void IsValidTopic()
        {
            var mockRepository = new Mock<IRepository<Topic>>();
            var validator = new TopicValidator(mockRepository.Object);
            var isValid = validator.IsValid(topic);

            Assert.IsTrue(isValid);
        }
    }
}
