using ImmRequest.Domain.UserManagement;
using ImmRequest.WebApi.Models.UserManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
                PassWord = "1235",
                UserName = "Another Example"
            };
        }

        [TestMethod]
        public void ToDomainTest()
        {
            var admin = model.ToDomain();
            Assert.AreEqual(model.Email, admin.Email);
            Assert.AreEqual(model.Username, admin.UserName);
            Assert.AreEqual(model.Password, admin.PassWord);

        }
    }
}
