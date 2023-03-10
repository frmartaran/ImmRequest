using ImmRequest.Domain.Enums;
using ImmRequest.Domain.Fields;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Helpers.Binders;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.WebApi.Tests.HelperTests
{
    [TestClass]
    public class BinderTest : CustomTypeConverter
    {
        Type type;

        [TestInitialize]
        public void Setup()
        {
            type = typeof(NumberFieldModel);
        }

        [TestMethod]
        public void IsAssignableTest()
        {
            Assert.IsTrue(CanConvert(type));
        }

        [TestMethod]
        public void IsNotAssignableTest()
        {
            Assert.IsFalse(CanConvert(typeof(Administrator)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoReaderTest()
        {
            ReadJson(null, type, "", new JsonSerializer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoSerializerTest()
        {
            var readermock = new Mock<JsonReader>().Object;
            ReadJson(readermock, type, "", null);
        }

        [TestMethod]
        public void NoTokenReturnNullTest()
        {
            var readermock = new Mock<JsonReader>();
            readermock.SetupGet(x => x.TokenType).Returns(JsonToken.Null);
            var result = ReadJson(readermock.Object, type, "", new JsonSerializer());
            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]

        public void JObjectIsNull()
        {
            Create(type, null);
        }

        [TestMethod]
        public void IsNumberFieldModelTest()
        {
            var jsonObject = JObject.Parse(@"{'dataType': '0'}");
            var model = Create(type, jsonObject);
            Assert.IsInstanceOfType(model, type);
        }

        [TestMethod]
        public void IsTextFieldModelTest()
        {
            var jsonObject = JObject.Parse(@"{'dataType': '1'}");
            var model = Create(type, jsonObject);
            Assert.IsInstanceOfType(model, typeof(TextFieldModel));
        }

        [TestMethod]
        public void IsDateTimeFieldModelTest()
        {
            var jsonObject = JObject.Parse(@"{'dataType': '2'}");
            var model = Create(type, jsonObject);
            Assert.IsInstanceOfType(model, typeof(DateTimeFieldModel));
        }

        [TestMethod]
        public void DefaultReturnsNull()
        {
            var jsonObject = JObject.Parse(@"{'dataType': '5'}");
            var model = Create(type, jsonObject);
            Assert.IsNull(model);
        }

    }
}
