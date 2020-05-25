﻿using ImmRequest.BusinessLogic.Logic.ImporterLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.BusinessLogic.Tests.ImporterTests
{
    [TestClass]
    public class ImporterTests
    {
        [TestMethod]
        public void GetImporterOptionsTest()
        {
            var logic = new ImporterLogic();
            var allImporterNames = logic.GetImporterOptions();
            var jsonTypeImporter = "Json Type Importer";
            var xmlTypeImporter = "XML Type Importer";

            Assert.IsTrue(allImporterNames.Contains(jsonTypeImporter));
            Assert.IsTrue(allImporterNames.Contains(xmlTypeImporter));

        }
    }
}