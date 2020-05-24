using ImmRequest.Importer.Domain;
using ImmRequest.Importer.Importers.Json.Binders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.Importer.Tests.JsonTests
{
    [TestClass]
    public class BinderTests
    {

        [DataTestMethod]
        [DataRow(typeof(Field), true)]
        [DataRow(typeof(Topic), false)]
        public void CanConvert(Type type, bool result)
        {
            var fieldConverter = new CustomTypeBinder<Field>();
            var can = fieldConverter.CanConvert(type);
            Assert.AreEqual(result, can);
        }
    }
}
