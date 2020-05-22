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
        private NumberField field;
        private BoolField field2;

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

            field = new NumberField
            {
                RangeStart = 1,
                RangeEnd = 10,
                Name = "Name"
            };

            field2 = new BoolField();
            type.AllFields.Add(field);
            type.AllFields.Add(field2);


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

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void CustomFieldWithoutAName()
        {
            field.Name = "";
            var validator = new TopicTypeValidator();
            validator.IsValid(type);
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]

        public void FieldsRangeIsInvalid()
        {
            field.RangeEnd = -5;
            var validator = new TopicTypeValidator();
            validator.IsValid(type);
        }



    }
}
