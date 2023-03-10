using ImmRequest.BusinessLogic.Exceptions;
using ImmRequest.BusinessLogic.Interfaces;
using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Controllers;
using ImmRequest.WebApi.Models.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImmRequest.WebApi.Tests.ControllerTests
{
    [TestClass]
    public class AdministratorControllerTest
    {
        private AdministratorModel model;
        private Administrator administrator;

        [TestInitialize]
        public void SetUp()
        {
            model = new AdministratorModel
            {
                Email = "example@example.com",
                Password = "1234",
                Username = "Example"
            };

            administrator = new Administrator
            {
                Email = "another@example.com",
                Password = "1235",
                UserName = "Another Example"
            };
        }

        [TestMethod]
        public void ToDomainTest()
        {
            var admin = model.ToDomain();
            Assert.AreEqual(model.Email, admin.Email);
            Assert.AreEqual(model.Username, admin.UserName);
            Assert.AreEqual(model.Password, admin.Password);

        }

        [TestMethod]
        public void ToDomainTestWithIdInTheModel()
        {
            model.Id = 2;
            var admin = model.ToDomain();
            Assert.AreEqual(model.Email, admin.Email);
            Assert.AreEqual(model.Username, admin.UserName);
            Assert.AreEqual(model.Password, admin.Password);
            Assert.AreEqual(model.Id.Value, admin.Id);

        }

        [TestMethod]
        public void ToModelTest()
        {
            var newModel = AdministratorModel.ToModel(administrator);
            Assert.AreEqual(administrator.Email, newModel.Email);
            Assert.AreEqual(administrator.UserName, newModel.Username);
            Assert.AreEqual(administrator.Password, newModel.Password);
            Assert.AreEqual(administrator.Id, newModel.Id.Value);
        }

        [TestMethod]
        public void GetTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Returns(administrator);

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Get(1);

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

            var okResponse = response as OkObjectResult;
            var modelResponse = okResponse.Value as AdministratorModel;
            var administratorResponse = modelResponse.ToDomain();

            Assert.AreEqual(administrator.Email, administratorResponse.Email);
            mockLogic.VerifyAll();
        }

        [TestMethod]
        public void GetNotFoundTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Get(It.IsAny<long>()))
                .Throws(new BusinessLogicException(""));

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Get(1);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void GetAllTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.GetAll())
                .Returns(new List<Administrator> { administrator });

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.GetAll();

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));

            var okResponse = response as OkObjectResult;
            var modelResponse = okResponse.Value as IEnumerable<AdministratorModel>;
            var administratorResponse = AdministratorModel.ToEntity(modelResponse);

            Assert.AreEqual(1, administratorResponse.Count());
            Assert.AreEqual(administrator.Email, administratorResponse.First().Email);
            mockLogic.VerifyAll();


        }

        [TestMethod]
        public void CreateAdministrator()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Create(It.IsAny<Administrator>()));
            mockLogic.Setup(m => m.Save());

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Create(model);

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void CreateInvalidAdministrator()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Create(It.IsAny<Administrator>()))
                .Throws(new ValidationException(""));

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Create(model);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();

        }


        [TestMethod]
        public void CreateNullTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Create(null);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void UpdateNullTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Update(1, null);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void UpdateTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Update(It.IsAny<Administrator>()));
            mockLogic.Setup(m => m.Save());

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Update(1, model);

            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void UpdateNotFoundTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Update(It.IsAny<Administrator>()))
                .Throws(new BusinessLogicException(""));

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Update(1, model);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void UpdateInvalidTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Update(It.IsAny<Administrator>()))
                .Throws(new ValidationException(""));

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Update(1, model);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();

        }

        [TestMethod]
        public void DeleteTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Delete(It.IsAny<long>()));
            mockLogic.Setup(m => m.Save());

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Delete(1);


            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
            mockLogic.VerifyAll();
        }

        [TestMethod]
        public void DeleteNotFoundTest()
        {
            var mockLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            mockLogic.Setup(m => m.Delete(It.IsAny<long>()))
                .Throws(new BusinessLogicException(""));

            var controller = new AdministratorController(mockLogic.Object);
            var response = controller.Delete(1);

            Assert.IsInstanceOfType(response, typeof(BadRequestObjectResult));
            mockLogic.VerifyAll();
        }
    }
}
