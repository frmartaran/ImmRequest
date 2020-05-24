using ImmRequest.Importer.Domain;
using ImmRequest.Importer.Importers.Json.Binders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImmRequest.Importer.Tests.JsonTests
{
    [TestClass]
    public class BinderTests
    {
        Type fieldType = typeof(Field);

        [DataTestMethod]
        [DataRow(typeof(Field), true)]
        [DataRow(typeof(Topic), false)]
        public void CanConvert(Type type, bool result)
        {
            var fieldConverter = new CustomTypeBinder<Field>();
            var can = fieldConverter.CanConvert(type);
            Assert.AreEqual(result, can);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ReaderIsNull()
        {
            var fieldConverter = new CustomTypeBinder<Field>();
            var field = fieldConverter.ReadJson(null, fieldType, new { }, new JsonSerializer());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SerializerIsNull()
        {
            var fieldConverter = new CustomTypeBinder<Field>();
            var readermock = new Mock<JsonReader>().Object;
            var field = fieldConverter.ReadJson(readermock, fieldType, new { }, null);
        }

        [TestMethod]
        public void DeserializeField()
        {
            var fieldConverter = new CustomTypeBinder<Field>();
            var field = @"{'name': 'Field', 'dataType': 'bool', 'rangeValues': []}";
            var reader = new JsonTextReader(new StringReader(field));
            var serializer = new JsonSerializer();
            var deserializedField = fieldConverter.ReadJson(reader, fieldType, new { },
                serializer);
            Assert.IsInstanceOfType(deserializedField, fieldType);
        }
    }
}
