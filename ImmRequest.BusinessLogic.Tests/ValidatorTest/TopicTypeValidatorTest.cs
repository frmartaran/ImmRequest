using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class TopicTypeValidatorTest
    {
        private Area area;
        private Topic topic;
        private TopicType type;

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
                Area = area
            };

            type = new TopicType
            {
                Name = "Type",
                ParentTopic = topic,
                AllFields = new List<BaseField>()
            };

        }

        [TestMethod]
        public void IsValidTest()
        {
            var validator = new TopicTypeValidator();
            var isValid = validator.IsValid(type);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InvalidWithoutTopic()
        {
            type.ParentTopic = null;
            var validator = new TopicTypeValidator();
            validator.IsValid(type);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void InvalidWithoutAName()
        {
            type.Name = "";
            var validator = new TopicTypeValidator();
            validator.IsValid(type);
        }


    }
}
