using ImmRequest.Domain.Fields;
using ImmRequest.WebApi.Helpers;
using ImmRequest.WebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ImmRequest.WebApi.Tests.HelperTests
{
    [TestClass]
    class BaseFieldModelFactoryTest
    {
        [TestMethod]
        public void ReturnsNewNumberFieldModel()
        {
            var numberField = new NumberField();
            var model = BaseFieldModelFactory.GetFieldModel(numberField);

            Assert.IsInstanceOfType(model, typeof(NumberFieldModel));
        }

        [TestMethod]
        public void ReturnsNewDateFieldModel()
        {
            var numberField = new DateTimeField();
            var model = BaseFieldModelFactory.GetFieldModel(numberField);

            Assert.IsInstanceOfType(model, typeof(DateTimeFieldModel));
        }

        [TestMethod]
        public void ReturnsNewTextFieldModel()
        {
            var numberField = new TextField();
            var model = BaseFieldModelFactory.GetFieldModel(numberField);

            Assert.IsInstanceOfType(model, typeof(TextField));
        }

    }
}
