﻿using ImmRequest.BusinessLogic.Logic.ImporterLogic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.Domain;
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
            var jsonTypeImporter = "Json Area Importer";
            var xmlTypeImporter = "Xml Area Importer";

            Assert.IsTrue(allImporterNames.Contains(jsonTypeImporter));
            Assert.IsTrue(allImporterNames.Contains(xmlTypeImporter));

        }

        [TestMethod]
        public void FindValidatorForArea()
        {
            var logic = new ImporterLogic();
            var areaValidator = logic.FindValidatorFor<Area>();
            Assert.IsInstanceOfType(areaValidator, typeof(AreaValidator));

        }

        [TestMethod]
        public void FindValidatorForTopic()
        {
            var logic = new ImporterLogic();
            var validator = logic.FindValidatorFor<Topic>();
            Assert.IsInstanceOfType(validator, typeof(Topic));

        }

        [TestMethod]
        public void FindValidatorForType()
        {
            var logic = new ImporterLogic();
            var validator = logic.FindValidatorFor<TopicType>();
            Assert.IsInstanceOfType(validator, typeof(TopicTypeValidator));

        }
    }
}
