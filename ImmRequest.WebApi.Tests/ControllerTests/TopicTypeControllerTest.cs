using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            var model = new NumberFieldModel().SetModel(numberField);

            Assert.AreEqual(model.Name, numberField.Name);
            var containsRange = model.RangeValues.Contains("1")
                && model.RangeValues.Contains("25");

            Assert.IsTrue(containsRange);
        }

        [TestMethod]
        public void NumberFieldToDomainTest()
        {
            var model = new NumberFieldModel().SetModel(numberField);
            var entity = model.ToDomain() as NumberField;

            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(1, entity.RangeStart);
            Assert.AreEqual(25, entity.RangeEnd);
        }


        [TestMethod]
        public void TextFieldToModelTest()
        {
            var model = new TextFieldModel().SetModel(textField);

            Assert.AreEqual(textField.Name, model.Name);
        }

        [TestMethod]
        public void TextFieldToDomainTest()
        {
            var model = new TextFieldModel().SetModel(textField);
            var entity = model.ToDomain();

            Assert.AreEqual(model.Name, entity.Name);
        }

        [TestMethod]
        public void DateTimeFieldToModelTest()
        {
            var model = new DateTimeFieldModel().SetModel(datesField);
            var startAsString = datesField.Start.ToString();
            var endAsString = datesField.End.ToString();

            Assert.AreEqual(datesField.Name, model.Name);

            var containsRange = model.RangeValues.Contains(startAsString)
                && model.RangeValues.Contains(endAsString);

            Assert.IsTrue(containsRange);
        }

        [TestMethod]
        public void DateTimeFieldToDomainTest()
        {
            var model = new DateTimeFieldModel().SetModel(datesField);
            var entity = model.ToDomain() as DateTimeField;

            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.RangeValues.First(), entity.Start.ToString());
            Assert.AreEqual(model.RangeValues.Skip(1).First(), entity.End.ToString());

        }

        [TestMethod]
        public void CreateTest()
        {
            var typeModel = TypeModel.ToModel(type);
            var logic = new Mock<ILogic<TopicType>>();
            logic.Setup(m => m.Create(It.IsAny<TopicType>()));
            var controller = new TypeController(logic.Object);
            var response = controller.Create(1, typeModel);

            Assert.IsInstanceOfType(typeModel, typeof(OkObjectResult));
        }
    }
}
