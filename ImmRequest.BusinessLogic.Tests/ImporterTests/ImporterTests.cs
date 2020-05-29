using ImmRequest.BusinessLogic.Helpers.Inputs;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic.ImporterLogic;
using ImmRequest.BusinessLogic.Validators;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var inputs = new AreaImporterInput
            (
                areaRepository: new Mock<IRepository<Area>>().Object,
                topicRepository: new Mock<IRepository<Topic>>().Object,
                areaValidator: new Mock<IValidator<Area>>().Object,
                topicValidator: new Mock<IValidator<Topic>>().Object,
                typeValidator: new Mock<IValidator<TopicType>>().Object
            );

            var logic = new ImporterLogic(inputs);
            var allImporterNames = logic.GetImporterOptions();
            var jsonTypeImporter = "Json Area Importer";
            var xmlTypeImporter = "Xml Area Importer";

            Assert.IsTrue(allImporterNames.Contains(jsonTypeImporter));
            Assert.IsTrue(allImporterNames.Contains(xmlTypeImporter));

        }

        [TestMethod]
        public void GetsImporterOfAnotherAssembly()
        {
            var inputs = new AreaImporterInput
            (
                areaRepository: new Mock<IRepository<Area>>().Object,
                topicRepository: new Mock<IRepository<Topic>>().Object,
                areaValidator: new Mock<IValidator<Area>>().Object,
                topicValidator: new Mock<IValidator<Topic>>().Object,
                typeValidator: new Mock<IValidator<TopicType>>().Object
            );

            var logic = new ImporterLogic(inputs);
            var allImporterNames = logic.GetImporterOptions();
            var otherImporter = "Test Importer";

            Assert.IsTrue(allImporterNames.Contains(otherImporter));
        }
    }
}
