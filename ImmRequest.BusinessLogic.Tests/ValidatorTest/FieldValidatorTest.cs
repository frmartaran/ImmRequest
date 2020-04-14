using ImmRequest.Domain.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace ImmRequest.BusinessLogic.Tests.ValidatorTest
{
    [TestClass]
    public class FieldValidatorTest
    {
        public TextField textRange;

        public DateTimeField dateTimeRange;

        public NumberField numberRange;

        [TestInitialize]
        public void Setup()
        {
            textRange = new TextField
            {
                Id = 1,
                Name = "TextRange",
                RangeValues = new List<string>
                {
                    "Credencial", "Cedula"
                },
                ParentTypeId = 1
            };
            dateTimeRange = new DateTimeField
            {
                Id = 1,
                Name = "DatetimeRange",
                Start = new DateTime(2020, 4, 1),
                End = new DateTime(2020, 4, 4),
                ParentTypeId = 1
            };
            numberRange = new NumberField
            {
                Id = 2,
                Name = "NumberRange",
                RangeStart = 1,
                RangeEnd = 4,
                ParentTypeId = 1
            };
        }

        [TestMethod]
        public void TextFieldIsValid()
        {
            var textToValidate = "Credencial";
            var isValid = textRange.Validate(textToValidate);
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void TextFieldIsInvalid()
        {
            var textToValidate = "Contribucion";
            var isValid = textRange.Validate(textToValidate);
            Assert.IsFalse(isValid);
        }
    }
}
