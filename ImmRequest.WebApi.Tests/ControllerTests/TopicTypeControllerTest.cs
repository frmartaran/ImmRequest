﻿using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ImmRequest.WebApi.Tests.ControllerTests
{
    [TestClass]
    public class TopicTypeControllerTest
    {
        private Area area;
        private Topic topic;
        private TopicType type;
        private NumberField numberField;
        private DateTimeField datesField;
        private TextField textField;

        [TestInitialize]
        public void SetUp()
        {
            numberField = new NumberField
            {
                Name = "Age Range",
                RangeStart = 1,
                RangeEnd = 25
            };

            datesField = new DateTimeField
            {
                Name = "Year of birth",
                Start = new DateTime(1995, 1, 1),
                End = new DateTime(2019, 1, 1)
            };

            textField = new TextField
            {
                Name = "Other Requirements",
                RangeValues = new List<string>
                {
                    "Credencial"
                }
            };

            area = new Area
            {
                Name = "Area Name"
            };

            topic = new Topic
            {
                Name = "Ne Name"
            };

            type = new TopicType
            {
                Name = "Type Name",
                AllFields = new List<BaseField>
                {
                    numberField,
                    datesField,
                    textField
                }
            };

        }

        [TestMethod]
        public void TypeToModelTest()
        {
            var model = TypeModel.ToModel(type);

            Assert.AreEqual(model.Name, type.Name);
        }

        [TestMethod]
        public void TypeModelToDomainTest()
        {
            var model = TypeModel.ToModel(type);
            var toEntity = model.ToDomain();

            Assert.AreEqual(model.Name, toEntity.Name);
        }

        [TestMethod]
        public void NumberFieldToModelTest()
        {
            var model = NumberFieldModel.ToModel(numberField);

            Assert.AreEqual(model.Name, numberField.Name);
            var containsRange = model.RangeValues.Contains("1")
                && model.RangeValues.Contains("25");

            Assert.IsTrue(containsRange);
        }

        [TestMethod]
        public void NumberFieldToDomainTest()
        {
            var model = NumberFieldModel.ToModel(numberField);
            var entity = model.ToDomain();

            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(1, entity.RangeStart);
            Assert.AreEqual(25, entity.RangeEnd);
        }


        [TestMethod]
        public void TextFieldToModelTest()
        {
            var model = TextFieldModel.ToModel(textField);

            Assert.AreEqual(textField.Name, model.Name);
        }

        [TestMethod]
        public void TextFieldToDomainTest()
        {
            var model = TextFieldModel.ToModel(textField);
            var entity = model.ToDomain();

            Assert.AreEqual(model.Name, entity.Name);
        }



    }
}
