using ImmRequest.Domain.Fields;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Helpers.Binders;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.WebApi.Tests.HelperTests
{
    [TestClass]
    public class BinderTest
    {
        CustomTypeConverter converter;
        Type type;

        [TestInitialize]
        public void Setup()
        {
            converter = new CustomTypeConverter();
            type = typeof(NumberFieldModel);
        }

        [TestMethod]
        public void IsAssignableTest()
        {
            Assert.IsTrue(converter.CanConvert(type));
        }

        [TestMethod]
        public void IsNotAssignableTest()
        {
            Assert.IsFalse(converter.CanConvert(typeof(Administrator)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoReaderTest()
        {
            converter.ReadJson(null, type, "", new JsonSerializer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NoSerializerTest()
        {
            var readermock = new Mock<JsonReader>().Object;
            converter.ReadJson(readermock, type, "", null);
        }


    }
}
