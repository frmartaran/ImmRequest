using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.WebApi.Tests.ControllerTests
{
    [TestClass]
    public class ImportControllerTests
    {
        [TestMethod]
        public void GetImportersTest()
        {
            var mockLogic = new Mock<IImporterLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.GetImporterOptions());

            var controller = new ImporterController(mockLogic.Object);
            var response = controller.Get();

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

            var asOK = response as OkObjectResult;
            var allNames = asOK.Value as List<string>;

            Assert.IsTrue(allNames.Contains("Json Area Importer"));
            Assert.IsTrue(allNames.Contains("Xml Area Importer"));

            mockLogic.VerifyAll();

        }
    }
}
