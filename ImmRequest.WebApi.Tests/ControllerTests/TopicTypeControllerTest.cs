using ImmRequest.Domain;
using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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



    }
}
