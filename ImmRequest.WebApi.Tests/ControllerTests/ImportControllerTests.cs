using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Helpers.Inputs;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.BusinessLogic.Logic.ImporterLogic;
using ImmRequest.DataAccess.Interfaces;
using ImmRequest.Domain;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ImmRequest.WebApi.Tests.ControllerTests
{
    [TestClass]
    public class ImportControllerTests
    {
        private const string Path = @"~\..\..\..\..\ControllerTests\Files";

        [TestMethod]
        public void GetImportersMockTest()
        {
            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.GetImporterOptions())
                .Returns(new List<string> { "Json Area Importer", "Xml Area Importer" });

            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Get();

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

            var asOK = response as OkObjectResult;
            var allNames = asOK.Value as List<string>;

            Assert.IsTrue(allNames.Contains("Json Area Importer"));
            Assert.IsTrue(allNames.Contains("Xml Area Importer"));

            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void GetImportersTest()
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
            var controller = new ImporterController(logic);
            var response = controller.Get();

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

            var asOK = response as OkObjectResult;
            var allNames = asOK.Value as List<string>;

            Assert.IsTrue(allNames.Contains("Json Area Importer"));
            Assert.IsTrue(allNames.Contains("Xml Area Importer"));

        }

        [TestMethod]
        public void ImportTest()
        {
            var file = new Mock<IFormFile>();
            var guidForUniqueName = Guid.NewGuid().ToString();
            file.SetupGet(f => f.FileName).Returns($"{guidForUniqueName}-ImportFile.json");

            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Import(It.IsAny<string>(), It.IsAny<string>()));

            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Import("Json Area Importer", file.Object);

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

        }

        [TestMethod]
        public void ImportValidationErrorTest()
        {
            var file = new Mock<IFormFile>();
            var guidForUniqueName = Guid.NewGuid().ToString();
            file.SetupGet(f => f.FileName).Returns($"{guidForUniqueName}-ImportFile.json");
        
            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Import(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new ValidationException(""));

            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Import("Json Area Importer", file.Object);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));

        }

        [TestMethod]
        public void ImportBusinessLogicErrorTest()
        {
            var file = new Mock<IFormFile>();
            var guidForUniqueName = Guid.NewGuid().ToString();
            file.SetupGet(f => f.FileName).Returns($"{guidForUniqueName}-ImportFile.json");

            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Import(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new BusinessLogicException(""));

            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Import("Json Area Importer", file.Object);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));

        }


        [TestMethod]
        public void ImportDeveloperExceptionTest()
        {
            var file = new Mock<IFormFile>();
            var guidForUniqueName = Guid.NewGuid().ToString();
            file.SetupGet(f => f.FileName).Returns($"{guidForUniqueName}-ImportFile.json");
           
            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Import(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new DeveloperException(""));

            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Import("Json Area Importer", file.Object);

            Assert.IsInstanceOfType(response, typeof(ObjectResult));

        }



        [TestMethod]
        public void FileIsNull()
        {
            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Import("Json Area Importer", null);

            Assert.IsInstanceOfType(response, typeof(BadRequestResult));

        }


    }
}
