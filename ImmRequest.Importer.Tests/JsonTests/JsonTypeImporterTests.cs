﻿using ImmRequest.Importer.Importers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.Importer.Tests.JsonTests
{
    [TestClass]
    public class JsonTypeImporterTests
    {
        [TestMethod]
        public void SuccessfulImport()
        {
            var path = $"{TestConstants.JsonPath}\\ImportType.json";
            var importer = new JsonTypeImporter(path);
            var types = importer.Import();

            Assert.AreEqual(1, types.Count);
            Assert.AreEqual(4, types.First().Fields.Count);
        }
    }
}